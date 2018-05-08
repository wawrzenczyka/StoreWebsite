using Microsoft.EntityFrameworkCore;
using ShopWebsite.Data;
using ShopWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Services
{
    public class OrderService: IOrderService
    {
        ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProductToCartAsync(CartItem newCartItem)
        {
            var cartItem = new CartItem()
            {
                Id = newCartItem.Id,
                ProductId = newCartItem.ProductId,
                Product = newCartItem.Product,
                UserId = newCartItem.UserId,
                Quantity = newCartItem.Quantity
            };

            _context.Add(cartItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartAsync(Guid userId)
        {
            var userItems = _context.CartItems.Where(item => item.UserId == userId);

            var result = from item in userItems
                         join product in _context.Products on item.ProductId equals product.Id
                         select new CartItemViewModel()
                         {
                             CartItem = item,
                             Product = product,
                             TotalValue = item.Quantity * product.Price
                         };

            return await result.ToArrayAsync();
        }
    }
}
