using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }

        [Display(Name = "Total value")]
        public decimal TotalValue
        {
            get
            {
                return CartItems.Sum(item => item.Quantity * item.Product.Price);
            }
        }
    }
}
