using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.WebApp.Models.Order
{
    public class OrderViewModel
    {
        public string MerchantOrderNo { get; set; }
        public string ChannelName { get; set; }
        public int ChannelId { get; set; }
        public string ChannelOrderNo { get; set; }
        public string Status { get; set; }
        public decimal TotalInclVat { get; set; }
    }
}
