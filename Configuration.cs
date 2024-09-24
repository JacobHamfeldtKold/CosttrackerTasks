using System;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Company.Project.Services
{
    public class MyService
    {
        private readonly IConfiguration _configuration;

        public string BaseUri => _configuration.GetValue<string>("ApiSettings:ApiName:Uri") + "/";

        public MyService(
            IConfiguration configuration
        )
        {
            _configuration = configuration;
        }
    }
}



//IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
//configurationBuilder.AddJsonFile("appsettings.Development.json");
//IConfiguration configuration = configurationBuilder.Build();
//var testing2 = configuration.GetSection("FixerIoValues");
//var testing3 = configuration.GetSection("FixerIoValues:access_key").Value;
//var testing4 = configuration.GetSection("FixerIoValues:BaseUrl").Value;
