using ChannelEngine.ClientApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi
{
    public interface IOrderClient
    {
        Task<Order> GetOrderAsync(string path);
        Task<IEnumerable<Order>> GetOrdersAsync(string filters = null);
    }
}
