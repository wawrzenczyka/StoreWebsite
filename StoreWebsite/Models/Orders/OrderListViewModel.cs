using ShopWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Models.Orders
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
