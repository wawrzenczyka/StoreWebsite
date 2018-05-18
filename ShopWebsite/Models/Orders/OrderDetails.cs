using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
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
        public int Quantity { get; set; }
    }
}
