using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public virtual ICollection<OrderContents> OrderContents { get; set; }
    }
}
