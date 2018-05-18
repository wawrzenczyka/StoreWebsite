using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal TotalValue
        {
            get
            {
                return CartItems.Sum(item => item.Quantity * item.Product.Price);
            }
        }
    }
}
