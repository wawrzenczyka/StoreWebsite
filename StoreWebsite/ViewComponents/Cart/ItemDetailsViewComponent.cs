using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.ViewComponents
{
    public class ItemDetailsViewComponent: ViewComponent
    {
        private readonly IProductService _productService;

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
