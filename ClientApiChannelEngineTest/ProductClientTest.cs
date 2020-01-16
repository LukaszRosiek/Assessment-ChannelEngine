using ClientApiChannelEngine;
using ClientApiChannelEngine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiChannelEngineTest
{
    [TestClass]
    public class ProductClientTest
    {
        private IClientConfig clientConfig;
        private IProductClient productClient;

        public void PrepareForTest()
        {
            clientConfig = new ClientConfigTest();
            productClient = new ProductClient(clientConfig);
        }

        [TestMethod]
        public async Task GetProductsAsyncTest()
        {
            PrepareForTest();
            var products = await productClient.GetProductsAsync();

            Assert.IsTrue(products != null);
            Assert.IsTrue(products.Count() > 0);
        }

        [TestMethod]
        public async Task GetProductAsyncTest()
        {
            PrepareForTest();
            var product = await productClient.GetProductAsync("001201-S");

            Assert.IsTrue(product != null);
        }

        [TestMethod]
        public async Task CreateUpdateDeleteProductAsyncTest()
        {
            int newStock = new Random().Next(50);
            PrepareForTest();
            var newProduct = PrepareSampleProduct();
            var creatingStatusCode = await productClient.CreateProductAsync(newProduct);
            Assert.AreEqual(creatingStatusCode, HttpStatusCode.OK);

            var product = await productClient.GetProductAsync(newProduct.MerchantProductNo);
            product.Stock = newStock;
            var updatingStatusCode = await productClient.UpdateProductAsync(product);
            Assert.AreEqual(updatingStatusCode, HttpStatusCode.OK);

            var productAfterUpdatingStock = await productClient.GetProductAsync(newProduct.MerchantProductNo);
            Assert.AreEqual(productAfterUpdatingStock.Stock, newStock);

            var deleatingStatusCode = await productClient.DeleteProductAsync(product.MerchantProductNo);
            Assert.AreEqual(deleatingStatusCode, HttpStatusCode.OK);
        }

        private Product PrepareSampleProduct()
        {
            Product product = new Product()
            {
                MerchantProductNo = "001201-XS",
                Name = "T-shirt met lange mouw BASIC petrol: XS",
                Stock = 10,
                Ean = null,
                Price = new decimal(6.50)
            };

            return product;
        }
    }
}
