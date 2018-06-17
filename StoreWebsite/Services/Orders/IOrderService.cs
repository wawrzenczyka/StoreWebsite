using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StoreWebsite.Models;
using StoreWebsite.Models.Orders;

namespace StoreWebsite.Services
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(Order order, List<OrderDetails> orderDetails);
        Task<IEnumerable<Order>> GetOrderListAsync(Guid userId);
        Task<Order> GetOrderAsync(Guid orderId);
        Task<bool> CancelOrderAsync(Order order);
    }
}
