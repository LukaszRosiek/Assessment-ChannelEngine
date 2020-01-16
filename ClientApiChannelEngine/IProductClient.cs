using ClientApiChannelEngine.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ClientApiChannelEngine
{
    public interface IProductClient
    {
        Task<HttpStatusCode> CreateProductAsync(Product product);
        Task<Product> GetProductAsync(string merchantProductNo);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<HttpStatusCode> UpdateProductAsync(Product product);
        Task<HttpStatusCode> DeleteProductAsync(string merchantProductNo);
        Task<IEnumerable<OrderedProduct>> GetTop5ProductFromOrdersAsync(IEnumerable<Order> listOfOrders);
    }
}
