using ChannelEngine.ClientApi;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.WebApp
{
    public class ClientConfig : IClientConfig
    {
        private readonly ClientConfiguration config;

        public ClientConfig(IOptions<ClientConfiguration> config)
        {
            this.config = config.Value;
                
        }
        public string GetApiKey()
        {
            return config.ApiKey;
        }

        public string GetBaseApiUrl()
        {
            return config.BaseApiUrl;
        }
    }
}
