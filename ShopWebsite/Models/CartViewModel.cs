using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartItemViewModel> CartItems { get; set; }
        public decimal TotalCartValue { get; set; }
    }
}
