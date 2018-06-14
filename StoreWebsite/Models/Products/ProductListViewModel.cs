using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
