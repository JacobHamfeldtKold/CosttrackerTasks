
using System.Web;
using System;
using System.Text.Json.Nodes;
using RestSharp;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Hangfire;
using DotnetWebApiWithEFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Hangfire");
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SampleDBContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();
app.UseHangfireDashboard();


app.MapGet("/", (IConfiguration config, SampleDBContext sampleDBContext) =>   
{
    return Results.Ok("Hakuna Matata");
});

app.MapPost("/InsertHistoricRate", ([FromBody] HistoricRate rate, IConfiguration config, SampleDBContext sampleDBContext) =>   
{
    Console.WriteLine("InsertRate Called");
    sampleDBContext.HistoricRate.Add(rate);
    sampleDBContext.SaveChanges();
    return Results.Ok("Historic rate inserted");
});

app.MapGet("/currencyConversion/{date?}", async ([FromBody] CurrencyInput currencyInput, string? date , IConfiguration config) =>  
{
    string pattern = "yyyy-MM-dd";
    DateTime parsedDate = DateTime.MaxValue;
    if (date != null && !DateTime.TryParseExact(date, pattern, null, DateTimeStyles.None, out parsedDate))
        return Results.BadRequest("Date should be in format: yyyy-MM-dd");

    if (date != null && parsedDate.Date >= DateTime.Now.Date)
        return Results.BadRequest("Date provided must be in the past when trying to get a historical currencyrate.");
        
    var access_Key = config.GetValue<string>("FixerIoValues:Access_Key");
    var convertEndpoint = config.GetValue<string>("FixerIoValues:ConvertEndpoint");

    var query = HttpUtility.ParseQueryString("");
    query["access_key"] = access_Key;
    query["from"] = $"{currencyInput.SellingCurrency}";
    query["to"] = $"{currencyInput.BuyingCurrency}";
    query["amount"] = $"{currencyInput.Amount}";
    if (date != null && parsedDate != DateTime.MaxValue)
        query["date"] = $"{parsedDate.ToString("yyyy-MM-dd")}";

    var uriBuilder = new UriBuilder(convertEndpoint);
    uriBuilder.Query = query.ToString();
    string finalUri = uriBuilder.ToString();

    HttpClient httpClient = new HttpClient();
    var response = await httpClient.GetAsync(new Uri(finalUri));
    string responseResult = response.Content.ReadAsStringAsync().Result;

    return Results.Ok(responseResult);
});

app.MapGet("/CreateRecurringJob", (IConfiguration config, SampleDBContext sampleDBContext) =>   
{
    RecurringJob.AddOrUpdate<CosttrackerTask.Hangfire.TestJob>(
        service => service.AddHistoricRate(),
          Cron.Daily); // "43 11 * * *"

    return Results.Ok("Hakuna Matata");
});


app.Run();

public class CurrencyInput
{
    public double Amount { get; set; }
    public string SellingCurrency { get; set; }
    public string BuyingCurrency { get; set; }

}
