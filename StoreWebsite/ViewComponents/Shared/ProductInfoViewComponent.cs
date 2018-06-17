using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Models;
using StoreWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.ViewComponents.Shared
{
    public class ProductInfoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            return await Task.Run(() => View(product));
        }
    }
}
