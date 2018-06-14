using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopWebsite.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public ProductType TypeCode { get; set; }
        public string Type
        {
            get
            {
                return TypeCode.GetEnumDescription();
            }
        }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual ProductImage Image { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }

    public enum ProductType
    {
        [Description("Beer")]
        Beer,
        [Description("Wine")]
        Wine,
        [Description("Wine")]
        Vodka
    }
}
