using ClientApiChannelEngine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApiChannelEngine
{
    public interface IOrderClient
    {
        Task<Order> GetOrderAsync(string path);
        Task<IEnumerable<Order>> GetOrdersAsync(string filters = null);
    }
}
