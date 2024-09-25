using DotnetWebApiWithEFCodeFirst.Models;
using Hangfire;
using Hangfire;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Web;


namespace CosttrackerTask.Hangfire
{
    public class TestJob
    {
        private readonly ILogger logger;

        private readonly IConfiguration config;

        public TestJob(ILogger<TestJob> Logger, IConfiguration Config) {
            logger = Logger;
            config = Config;
            } 


        public async Task AddHistoricRate()
        {
            var access_Key = config.GetValue<string>("FixerIoValues:Access_Key");
            var latestExchangeRateEndpoint = config.GetValue<string>("FixerIoValues:LatestExchangeRateEndpoint");

            var query = HttpUtility.ParseQueryString("");
            query["access_key"] = access_Key;
            query["symbols"] = $"USD,EUR,AUD,CAD,PLN,MXN";
            query["format"] = "1";

            var uriBuilder = new UriBuilder(latestExchangeRateEndpoint);
            uriBuilder.Query = query.ToString();
            string finalUri = uriBuilder.ToString();

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(new Uri(finalUri));

            string responseResult = response.Content.ReadAsStringAsync().Result;
            string currencyRates = "";
            int indexOfRatesInfo = responseResult.IndexOf("\"rates\":");
            if (indexOfRatesInfo != -1)
                currencyRates = "{" + responseResult.Substring(indexOfRatesInfo);
            else
                return;  //Todo write to log

            var data = JObject.Parse(currencyRates);
            var location = data.SelectToken($"rates");
            double sellingCurrencyRate = location.Value<double>("USD");
            double buyingCurrencyRate = location.Value<double>("EUR");


            HistoricRate historicRate = new HistoricRate()
            {
                Date = DateTime.Now,
                BaseCurrency = "EUR",
                USD = location.Value<double>("USD"),
                EUR = location.Value<double>("EUR"),
                AUD = location.Value<double>("AUD"),
                CAD = location.Value<double>("CAD"),
                PLN = location.Value<double>("PLN"),
                MXN = location.Value<double>("MXN")
            };

            HttpClient httpClient2 = new HttpClient();

             HttpResponseMessage response2 = await httpClient2.PostAsJsonAsync(
             $"{config.GetValue<string>("rootUrl")}InsertHistoricRate", historicRate);

            string responseResult2 = response2.Content.ReadAsStringAsync().Result;
        }

    }
}
