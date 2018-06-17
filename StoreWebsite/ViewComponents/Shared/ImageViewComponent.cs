using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.ViewComponents.Shared
{
    public class ImageViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ProductImage image)
        {
            return await Task.Run(() => View(image));
        }
    }
}
