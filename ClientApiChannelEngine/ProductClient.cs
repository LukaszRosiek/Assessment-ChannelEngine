using ClientApiChannelEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientApiChannelEngine
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient client;
        private readonly string baseUri;
        private readonly string additionalApiKey;

        public ProductClient(IClientConfig clientConfig)
        {
            this.baseUri = $"{clientConfig.GetBaseApiUrl()}/api/v2";
            this.additionalApiKey = clientConfig.GetApiKey();
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<HttpStatusCode> CreateProductAsync(Product product)
        {
            var uri = API.Product.UpsertProducts(baseUri, additionalApiKey);
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, new[] { product });
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<Product> GetProductAsync(string merchantProductNo)
        {
            Product product = null;
            var uri = API.Product.GetProduct(baseUri,merchantProductNo, additionalApiKey);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product.Content;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            Products products = null;
            var uri = API.Product.GetProducts(baseUri, additionalApiKey);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<Products>();
            }
            return products.Content;
        }

        public async Task<HttpStatusCode> UpdateProductAsync(Product product)
        {
            var uri = API.Product.UpsertProducts(baseUri, additionalApiKey);
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, new[] { product });
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteProductAsync(string merchantProductNo)
        {
            var uri = API.Product.DeleteProduct(baseUri, merchantProductNo, additionalApiKey);
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
