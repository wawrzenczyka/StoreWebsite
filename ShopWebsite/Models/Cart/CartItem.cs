using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }

        [Required]
        public Guid UserId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
