using ChannelEngine.ClientApi;
using ChannelEngine.ClientApi.Helpers;
using ChannelEngine.ClientApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.ConsoleApp
{
    public class BusinessLogicExecution : IBusinessLogicExecution
    {
        private IClientConfig clientConfig;
        private IProductClient productClient;
        private IOrderClient orderClient;
        public BusinessLogicExecution(IProductClient productClient, IOrderClient orderClient, IClientConfig clientConfig)
        {
            this.productClient = productClient;
            this.orderClient = orderClient;
            this.clientConfig = clientConfig;
        }
        public async Task RunAsync()
        {
            try
            {
                var orders = await orderClient.GetOrdersAsync($"status={OrderStatus.IN_PROGRESS}");
                ConsoleWriteOrders(orders);

                var products = await productClient.GetTop5ProductFromOrdersAsync(orders);
                ConsoleWriteProducts(products);

                var merchantProductNoToUpdate = ChooseMerchantProductNoToUpdate(products);
                await SetStockOfThisProductAsync(merchantProductNoToUpdate);
                Console.WriteLine("Press Enter to finish");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private void ConsoleWriteOrders(IEnumerable<Order> orders)
        {
            Console.WriteLine("Orders with status IN_PROGRESS:");
            Console.WriteLine("ChannelName/ ChannelOrderNo/ Status/ TotalInclVat");
            foreach (var o in orders)
            {
                Console.WriteLine($"{o.ChannelName}/ {o.ChannelOrderNo}/ {o.Status}/ {o.TotalInclVat}");
            }
        }

        private void ConsoleWriteProducts(IEnumerable<OrderedProduct> orderedProducts)
        {
            Console.WriteLine("Top 5 products sold:");
            Console.WriteLine("No/Name/ EAN/ Total quantity");
            int i = 1;
            foreach (var p in orderedProducts)
            {
                Console.WriteLine($"{i}/{p.Name}/ {p.Ean}/ {p.TotalQuantity}");
                i++;
            }
        }

        private string ChooseMerchantProductNoToUpdate(IEnumerable<OrderedProduct> orderedProducts)
        {
            var merchantProductNo = string.Empty;
            Console.WriteLine("Which product's stock you want to update?(pick 1 to 5)");
            var key = Console.ReadLine();
            int row = -1;
            while (!(Int32.TryParse(key, out row) && row >= 1 && row <= 5))
            {
                Console.WriteLine("Please type number from 1 to 5");
                key = Console.ReadLine();
            }
            if (orderedProducts.ElementAt(row - 1) != null)
                merchantProductNo = orderedProducts.ElementAt(row - 1).MerchantProductNo;

            return merchantProductNo;
        }

        private async Task SetStockOfThisProductAsync(string merchantProductNoToUpdate)
        {
            Product product = await productClient.GetProductAsync(merchantProductNoToUpdate);
            product.Stock = AskAndGetNewStockOfProduct();
            await productClient.UpdateProductAsync(product);
            Product productAfterUpdate = await productClient.GetProductAsync(product.MerchantProductNo);
            Console.WriteLine("Updated products: (MerchantProductNo/ Name/ Ean/ Stock)");
            Console.WriteLine($"{productAfterUpdate.MerchantProductNo}/ {productAfterUpdate.Name}/ {productAfterUpdate.Ean}/ {productAfterUpdate.Stock}");

        }

        private int AskAndGetNewStockOfProduct()
        {
            Console.WriteLine("Enter the new stock of choosen product");
            var key = Console.ReadLine();
            int stock = -1;
            while (!(Int32.TryParse(key, out stock) && stock >= 0))
            {
                Console.WriteLine("Please type integer number bigger or equal to 0!");
                key = Console.ReadLine();
            }
            return stock;
        }
    }
}
