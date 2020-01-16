using ChannelEngine.ClientApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi.Test
{
    [TestClass]
    public class OrderClientTest
    {
        private IClientConfig clientConfig;
        private IOrderClient orderClient;

        private void PrepareForTest()
        {
            clientConfig = new ClientConfigTest();
            orderClient = new OrderClient(clientConfig);
        }

        [TestMethod]
        public async Task GetOrdersAsyncTest()
        {
            PrepareForTest();
            IEnumerable<Order> orders = await orderClient.GetOrdersAsync();

            Assert.IsTrue(orders != null);
            Assert.IsTrue(orders.Count() > 0);
        }

        [TestMethod]
        public async Task GetOrdersWithFiltersAsyncTest()
        {
            PrepareForTest();
            string filters = "status=IN_PROGRESS";
            var orders = await orderClient.GetOrdersAsync(filters);

            Assert.IsTrue(orders != null);
            Assert.IsTrue(orders.Count() > 0);
        }
    }
}
