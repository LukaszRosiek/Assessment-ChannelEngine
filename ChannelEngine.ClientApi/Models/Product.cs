﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.ClientApi.Models
{
    public class Product
    {
        public bool IsActive { get; set; }
        public string MerchantProductNo { get; set; }
        public string Name { get; set; }
        public string Ean { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Product Content { get; set; }
    }
}
