using ClientApiChannelEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiChannelEngineTest
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
            var orders = await orderClient.GetOrdersAsync();

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
