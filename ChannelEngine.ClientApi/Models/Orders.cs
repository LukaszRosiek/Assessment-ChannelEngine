using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.ClientApi.Models
{
    public class Orders
    {
        public IEnumerable<Order> Content { get; set; }
    }
}
