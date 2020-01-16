using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class OrderAndProducts
    {
        public IEnumerable<ClientApiChannelEngine.Models.Order> Orders { get; set; }
        public IEnumerable<ClientApiChannelEngine.Models.OrderedProduct> OrderedProducts { get; set; }
    }
}
