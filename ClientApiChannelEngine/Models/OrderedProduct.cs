using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiChannelEngine.Models
{
    public class OrderedProduct
    {
        public string MerchantProductNo { get; set; }
        public int Quantity { get; set; }
        public int TotalQuantity { get; set; }
        public string Name { get; set; }
        public string Ean { get; set; }
    }
}
