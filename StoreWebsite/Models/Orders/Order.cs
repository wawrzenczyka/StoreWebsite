using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Models
{
    public class Order
    {
        [Display(Name = "Order number")]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required, Display(Name = "Ordered date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Shipped date")]
        public DateTime? OrderShippedDate { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Display(Name = "Status")]
        public OrderStatus StatusCode { get; set; }

        [Display(Name = "Postal code")]
        public string OrderStatus
        {
            get
            {
                return StatusCode.GetEnumDescription();
            }
        }

        [Display(Name = "Message")]
        public string Description { get; set; }

        [Display(Name = "Total price")]
        public decimal TotalPrice
        {
            get
            {
                return OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Product.Price * od.Quantity);
            }
        }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }

    public enum OrderStatus
    {
        [Description(""), Display(Name = "")]
        None,
        [Description("New")]
        New,
        [Description("Confirmed")]
        Confirmed,
        [Description("In progress"), Display(Name = "In progress")]
        InProgress,
        [Description("Shipped")]
        Shipped,
        [Description("Completed")]
        Completed,
        [Description("Cancelled")]
        Cancelled,
        [Description("All")]
        All
    }
}
