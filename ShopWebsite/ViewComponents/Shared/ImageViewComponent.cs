using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.ViewComponents.Shared
{
    public class ImageViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ProductImage image)
        {
            return await Task.Run(() => View(image));
        }
    }
}
