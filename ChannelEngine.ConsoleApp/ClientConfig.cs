using ChannelEngine.ClientApi;
using Microsoft.Extensions.Configuration;

namespace ChannelEngine.ConsoleApp
{
    public class ClientConfig : IClientConfig
    {
        IConfiguration config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true, true)
          .Build();

        public string GetApiKey() => config["ApiKey"];

        public string GetBaseApiUrl() => config["BaseApiUrl"];
    }
}
