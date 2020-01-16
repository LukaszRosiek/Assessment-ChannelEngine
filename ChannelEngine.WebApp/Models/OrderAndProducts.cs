using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.WebApp.Models
{
    public class OrderAndProducts
    {
        public IEnumerable<ChannelEngine.ClientApi.Models.Order> Orders { get; set; }
        public IEnumerable<ChannelEngine.ClientApi.Models.OrderedProduct> OrderedProducts { get; set; }
    }
}
