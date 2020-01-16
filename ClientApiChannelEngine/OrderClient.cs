using ChannelEngine.ClientApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi
{
    public class OrderClient : ApiClientBase, IOrderClient
    {
        public OrderClient(IClientConfig clientConfig) : base(clientConfig)
        {
        }

        public async Task<Order> GetOrderAsync(string path)
        {
            Order order = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                order = await response.Content.ReadAsAsync<Order>();
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string filters = null)
        {
            string uri = filters == null ? urlResolver.OrderUrl.GetOrders() : urlResolver.OrderUrl.GetOrders(baseUri, filters, apiKey);

            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to load data.");
            }

            return (await response.Content.ReadAsAsync<Orders>()).Content;
        }
    }
}
