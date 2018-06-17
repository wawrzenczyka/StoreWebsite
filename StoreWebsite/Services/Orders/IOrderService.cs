using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ShopWebsite.Models;
using StoreWebsite.Models.Orders;

namespace ShopWebsite.Services
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(Order order, List<OrderDetails> orderDetails);
        Task<IEnumerable<Order>> GetOrderListAsync(Guid userId);
    }
}
