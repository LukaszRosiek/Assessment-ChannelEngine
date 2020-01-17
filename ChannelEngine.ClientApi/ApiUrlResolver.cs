using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.ClientApi
{
    /// <summary>
    /// Class to build URI
    /// </summary>
    public class ApiUrlResolver
    {
        private static ApiUrlResolver _instance;
        private static string baseUri;
        private static string apiKey;

        private ApiUrlResolver(IClientConfig config)
        {
            baseUri = config.GetBaseApiUrl();
            apiKey = config.GetApiKey();

            OrderUrl = new Order();
            ProductUrl = new Product();
        }

        public Order OrderUrl { get; private set; }
        public Product ProductUrl { get; private set; }

        internal static ApiUrlResolver Get(IClientConfig clientConfig)
        {
            if (_instance == null)
            {
                _instance = new ApiUrlResolver(clientConfig);
            }

            return _instance;
        }

        public class Order
        {
            public string GetOrders() => $"{baseUri}/orders?apikey={apiKey}";
            public string GetOrders(string filters)
                => $"{baseUri}/orders?{filters}&apikey={apiKey}";
        }

        public class Product
        {
            public string GetProducts() => $"{baseUri}/products?apikey={apiKey}";
            public string UpsertProducts() => $"{baseUri}/products?apikey={apiKey}";
            public string GetProduct(string merchantProductNo)
                => $"{baseUri}/products/{merchantProductNo}?apikey={apiKey}";
            public string DeleteProduct(string merchantProductNo)
                => $"{baseUri}/products/{merchantProductNo}?apikey={apiKey}";
        }
    }
}
