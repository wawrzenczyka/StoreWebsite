using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;
using ShopWebsite.Services;

namespace ShopWebsite.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IProductService _productService;

        public OrdersController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Order(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }
    }
}