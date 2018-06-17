using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Data;
using ShopWebsite.Models;
using StoreWebsite.Models.Orders;

namespace ShopWebsite.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrderAsync(Order order, List<OrderDetails> orderDetails)
        {
            await _context.AddAsync(order);

            foreach (OrderDetails itemDetails in orderDetails)
            {
                await _context.AddAsync(itemDetails);
            }

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1 + orderDetails.Count;
        }

        public async Task<IEnumerable<Order>> GetOrderListAsync(Guid userId)
        {
            return await _context.Orders
                .Where(order => order.UserId == userId)
                .ToArrayAsync();
        }
    }
}
