using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<OrderContents> OrderContents { get; set; }
    }
}
