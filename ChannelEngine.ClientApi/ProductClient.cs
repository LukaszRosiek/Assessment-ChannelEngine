using ChannelEngine.ClientApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi
{
    public class ProductClient : ApiClientBase, IProductClient
    {
        public ProductClient(IClientConfig clientConfig) : base(clientConfig)
        {
        }

        public async Task<HttpStatusCode> CreateProductAsync(Product product)
        {
            var uri = urlResolver.ProductUrl.UpsertProducts();
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, new[] { product });
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<Product> GetProductAsync(string merchantProductNo)
        {
            var uri = urlResolver.ProductUrl.GetProduct(merchantProductNo);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to load Product data.");
            }

            return (await response.Content.ReadAsAsync<Product>()).Content;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var uri = urlResolver.ProductUrl.GetProducts();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to load Products data.");
            }

            return (await response.Content.ReadAsAsync<Products>()).Content;
        }

        public async Task<HttpStatusCode> UpdateProductAsync(Product product)
        {
            var uri = urlResolver.ProductUrl.UpsertProducts();
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, new[] { product });
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteProductAsync(string merchantProductNo)
        {
            var uri = urlResolver.ProductUrl.DeleteProduct(merchantProductNo);
            HttpResponseMessage response = await client.DeleteAsync(uri);

            return response.StatusCode;
        }

        public async Task<IEnumerable<OrderedProduct>> GetTop5ProductFromOrdersAsync(IEnumerable<Order> listOfOrders)
        {
            var products = GetTop5Product(listOfOrders);
            var updatedproducts = await UpdateProductAsync(products);

            return updatedproducts;
        }

        private IEnumerable<OrderedProduct> GetTop5Product(IEnumerable<Order> orders)
        {
            var orderedProducts = new List<OrderedProduct>();
            foreach (var o in orders)
            {
                foreach (var p in o.Lines)
                {
                    if (!orderedProducts.Where(x => x.MerchantProductNo == p.MerchantProductNo).Any())
                        orderedProducts.Add(new OrderedProduct() { MerchantProductNo = p.MerchantProductNo, TotalQuantity = p.Quantity });
                    else
                        orderedProducts.Where(x => x.MerchantProductNo == p.MerchantProductNo).FirstOrDefault().TotalQuantity += p.Quantity;
                }
            }

            return orderedProducts.OrderByDescending(x => x.TotalQuantity).Take(5);
        }
        private async Task<IEnumerable<OrderedProduct>> UpdateProductAsync(IEnumerable<OrderedProduct> products)
        {
            var listOfUpdated = new List<OrderedProduct>();
            foreach (var product in products)
            {
                var productDetails = await GetProductAsync(product.MerchantProductNo);
                listOfUpdated.Add(new OrderedProduct()
                {
                    MerchantProductNo = product.MerchantProductNo,
                    TotalQuantity = product.TotalQuantity,
                    Name = productDetails.Name,
                    Ean = productDetails.Ean
                });
            }

            return listOfUpdated;
        }
    }
}
