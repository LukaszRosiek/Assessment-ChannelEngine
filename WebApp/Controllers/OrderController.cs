using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChannelEngine.ClientApi;
using ChannelEngine.ClientApi.Helpers;
using ChannelEngine.ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using ChannelEngine.WebApp.Models;
using ChannelEngine.WebApp.Models.Order;

namespace ChannelEngine.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private IProductClient productClient;
        private IOrderClient orderClient;
        private IMapper mapper;

        public OrderController(IOrderClient orderClient, IProductClient productClient)
        {
            this.orderClient = orderClient;
            this.productClient = productClient;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderViewModel>());
            mapper = config.CreateMapper();
        }
        public async Task<IActionResult> IndexAsync()
        {
            var orders = await orderClient.GetOrdersAsync();

            IEnumerable<OrderViewModel> ordersViewModel = mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

            return View(ordersViewModel);


        }

        public async Task<IActionResult> ListOfResultAsync(string filters = null)
        {
            if (filters == null)
                filters = "status=" + OrderStatus.IN_PROGRESS;

            var orders = await orderClient.GetOrdersAsync(filters);

            var products = await productClient.GetTop5ProductFromOrdersAsync(orders);

            var viewOject = new OrderAndProducts()
            {
                Orders = orders,
                OrderedProducts = products
            };

            return View(viewOject);
        }
    }
}