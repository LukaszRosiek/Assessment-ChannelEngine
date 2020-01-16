using ChannelEngine.ClientApi;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChannelEngine.ConsoleApp
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            var service = _serviceProvider.GetService<IBusinessLogicExecution>();
            service.RunAsync().GetAwaiter().GetResult();
            DisposeServices();
        }
        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBusinessLogicExecution, BusinessLogicExecution>();
            services.AddSingleton<IProductClient, ProductClient>();
            services.AddSingleton<IOrderClient, OrderClient>();
            services.AddSingleton<IClientConfig, ClientConfig>();

            _serviceProvider = services.BuildServiceProvider();
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

    }
}
