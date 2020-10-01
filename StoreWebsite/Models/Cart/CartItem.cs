using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "Total price")]
        public decimal TotalPrice
        {
            get
            {
                return Product != null ? Product.Price * Quantity : 0;
            }
        }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
