using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models.Products
{
    public class ProductImage
    {
        public Guid ProductId { get; set; }
        public byte[] Image { get; set; }
    }
}
