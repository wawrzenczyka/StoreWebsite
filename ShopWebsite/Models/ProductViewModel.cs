using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
