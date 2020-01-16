using ClientApiChannelEngine.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientApiChannelEngine
{
    public class OrderClient : IOrderClient
    {
        private readonly HttpClient client;
        private readonly string baseUri;
        private readonly string additionalApiKey;

        public OrderClient(IClientConfig clientConfig)
        {
            this.baseUri = $"{clientConfig.GetBaseApiUrl()}/api/v2";
            this.additionalApiKey = clientConfig.GetApiKey();
            this.client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
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
            string uri;
            Orders orders = null;
            HttpResponseMessage response;
            if (filters == null)
            {
                uri = API.Order.GetOrders(baseUri, additionalApiKey);
                response = await client.GetAsync(uri);
            }
            else
            {
                uri = API.Order.GetOrders(baseUri, filters, additionalApiKey);
                response = await client.GetAsync(uri);
            }

            if (response.IsSuccessStatusCode)
            {
                orders = await response.Content.ReadAsAsync<Orders>();
            }
            return orders.Content;
        }
    }
}
