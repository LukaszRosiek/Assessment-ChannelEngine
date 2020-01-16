using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApiChannelEngine
{
    /// <summary>
    /// Class to build URI
    /// </summary>
    public static class API
    {
        public static class Order
        {
            public static string GetOrders(string baseUri, string apikey) => $"{baseUri}/orders?apikey={apikey}";
            public static string GetOrders(string baseUri, string filters, string apikey) 
                => $"{baseUri}/orders?{filters}&apikey={apikey}";
        }

        public static class Product
        {
            public static string GetProducts(string baseUri, string apikey) => $"{baseUri}/products?apikey={apikey}";
            public static string UpsertProducts(string baseUri, string apikey) => $"{baseUri}/products?apikey={apikey}";
            public static string GetProduct(string baseUri, string merchantProductNo, string apikey) 
                => $"{baseUri}/products/{merchantProductNo}?apikey={apikey}";
            public static string DeleteProduct(string baseUri, string merchantProductNo, string apikey)
                => $"{baseUri}/products/{merchantProductNo}?apikey={apikey}";
        }
    }
}
