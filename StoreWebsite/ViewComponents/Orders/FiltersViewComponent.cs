using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.ViewComponents.Orders
{
    public class FiltersViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(OrderFilters filters)
        {
            return await Task.Run(() => View(filters));
        }
    }
}
