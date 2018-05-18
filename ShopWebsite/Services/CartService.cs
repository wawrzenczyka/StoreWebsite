using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Data;
using ShopWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Services
{
    public class CartService: ICartService
    {
        readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProductToCartAsync(CartItem newCartItem)
        {
            var cartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
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

        public async Task<CartItemViewModel> GetCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _context.CartItems
                .FirstAsync(item => item.Id == cartItemId);

            var product = await _context.Products
                .FirstAsync(p => cartItem.ProductId == p.Id);

            return new CartItemViewModel()
            {
                CartItem = cartItem,
                Product = product,
                TotalValue = cartItem.Quantity * product.Price
            };
        }

        public async Task<bool> RemoveCartItem(CartItemViewModel cartItemViewModel)
        {
            _context.CartItems.Remove(cartItemViewModel.CartItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public Task<CartItem> CheckIfCartContains(Guid product, Guid user)
        {
            return _context.CartItems
                .FirstOrDefaultAsync(item => item.ProductId == product && item.UserId == user);
        }

        public async Task<bool> UpdateCartItem(CartItem cartItem)
        {
            CartItem oldItem = await _context.CartItems.FirstAsync(item => item.Id == cartItem.Id);
            oldItem.Quantity = cartItem.Quantity;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1 || saveResult == 0;
        }
    }
}
