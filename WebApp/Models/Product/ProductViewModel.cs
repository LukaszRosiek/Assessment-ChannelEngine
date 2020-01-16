using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Product
{
    public class ProductViewModel
    {
        public bool IsActive { get; set; }
        public string MerchantProductNo { get; set; }
        public string Name { get; set; }
        public string Ean { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
