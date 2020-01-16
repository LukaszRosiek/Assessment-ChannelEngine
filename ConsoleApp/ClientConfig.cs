using ClientApiChannelEngine;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class ClientConfig : IClientConfig
    {
        IConfiguration config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true, true)
          .Build();
        public string GetApiKey()
        {
            return config["ApiKey"];
        }

        public string GetBaseApiUrl()
        {
            return config["BaseApiUrl"];
        }
    }
}
