using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi
{
    public abstract class ApiClientBase
    {
        protected readonly HttpClient client;
        protected readonly string baseUri;
        protected readonly string apiKey;
        protected ApiUrlResolver urlResolver;

        protected ApiClientBase(IClientConfig clientConfig)
        {
            this.baseUri = $"{clientConfig.GetBaseApiUrl()}/api/v2";
            this.apiKey = clientConfig.GetApiKey();
            this.urlResolver = ApiUrlResolver.Get(clientConfig);
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
