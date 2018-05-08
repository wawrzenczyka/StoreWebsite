using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;
using ShopWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.ViewComponents
{
    public class OrderFormViewComponent: ViewComponent
    {
        private IProductService _productService;

        public OrderFormViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(new OrderDetails()
            {
                Product = product,
                ProductId = Id
            });
        }
    }
}
