using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class CartItemViewModel
    {
        public CartItem CartItem { get; set; }
        public Product Product { get; set; }
        public decimal TotalValue { get; set; }
    }
}
