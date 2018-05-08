using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.ViewComponents
{
    public class ItemDetailsViewComponent: ViewComponent
    {
        private IProductService _productService;

        public ItemDetailsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }
    }
}
