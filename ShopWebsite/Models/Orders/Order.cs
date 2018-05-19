using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime OrderShippedDate { get; set; }

        [Required]
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Required]
        public OrderStatus StatusCode { get; set; }
        public string OrderStatus
        {
            get
            {
                return StatusCode.GetEnumDescription();
            }
        }
        public string Description { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }

    public enum OrderStatus
    {
        [Description("Confirmed")]
        Confirmed,
        [Description("In progress")]
        InProgress,
        [Description("Shipped")]
        Shipped,
        [Description("Delivered")]
        Delivered
    }
}
