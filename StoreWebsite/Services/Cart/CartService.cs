using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using StoreWebsite.Data;
using StoreWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Services
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

        public async Task<IEnumerable<CartItem>> GetCartAsync(Guid userId)
        {
            return await _context.CartItems
                .Where(item => item.UserId == userId)
                .Include(item => item.Product)
                    .ThenInclude(product => product.Image)
                .OrderBy(item => item.Product.Name)
                .ToArrayAsync();
        }

        public Task<CartItem> GetCartItemAsync(Guid cartItemId)
        {
            return _context.CartItems
                .Include(item => item.Product)
                .FirstAsync(item => item.Id == cartItemId);
        }

        public async Task<bool> RemoveCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public Task<CartItem> FindItemAsync(Guid product, Guid user)
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

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await GetCartAsync(userId);

            foreach(var cartItem in cart)
            {
                bool successful = await RemoveCartItem(cartItem);
            }
        }
    }
}
