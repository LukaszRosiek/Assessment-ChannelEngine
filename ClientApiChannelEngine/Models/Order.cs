using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi.Models
{
    public class Order
    {
        public string MerchantOrderNo { get; set; }
        public string ChannelName { get; set; }
        public int ChannelId { get; set; }
        public string ChannelOrderNo { get; set; }
        public string Status { get; set; }
        public decimal TotalInclVat { get; set; }
        public IEnumerable<OrderedProduct> Lines { get; set; }
        public IEnumerable<Order> Content { get; set; }
    }
}
