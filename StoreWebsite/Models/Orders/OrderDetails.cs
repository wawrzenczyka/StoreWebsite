using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Models
{
    public class OrderDetails
    {
        [Key]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Key]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Total price")]
        public decimal TotalPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}
