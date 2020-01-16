using AutoMapper;
using ChannelEngine.ClientApi;
using ChannelEngine.ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngine.WebApp.Models.Product;

namespace ChannelEngine.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductClient productClient;
        private IMapper mapper;
        public ProductController(IProductClient productClient)
        {
            this.productClient = productClient;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, Product>();
            });
            mapper = config.CreateMapper();
        }
        public async Task<ActionResult> IndexAsync()
        {
            var products = await productClient.GetProductsAsync();

            IEnumerable<ProductViewModel> productsViewModel = mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return View(productsViewModel);
        }

        public async Task<ActionResult> DetailsAsync(string merchantProductNo)
        {
            var product = await productClient.GetProductAsync(merchantProductNo);

            var productViewModel = mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        public ActionResult Create()
        {
            var productViewModel = new ProductViewModel();

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProductViewModel productViewModel)
        {
            try
            {
                var product = mapper.Map<Product>(productViewModel);

                var status = await productClient.CreateProductAsync(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditAsync(string merchantProductNo)
        {
            var product = await productClient.GetProductAsync(merchantProductNo);

            var productViewModel = mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(ProductViewModel productViewModel)
        {
            try
            {
                var product = mapper.Map<Product>(productViewModel);

                var status = await productClient.UpdateProductAsync(product);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<ActionResult> DeleteAsync(string merchantProductNo)
        {
            var status = await productClient.DeleteProductAsync(merchantProductNo);

            return RedirectToAction("Index");
        }
    }
}