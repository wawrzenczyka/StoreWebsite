using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Models.Products
{
    public class ProductImage
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}
