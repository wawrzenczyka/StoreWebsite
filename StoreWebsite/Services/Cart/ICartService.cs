using Microsoft.IdentityModel.Clients.ActiveDirectory;
using ShopWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Services
{
    public interface ICartService
    {
        Task<bool> AddProductToCartAsync(CartItem newCartItem);
        Task<IEnumerable<CartItem>> GetCartAsync(Guid userId);
        Task<CartItem> GetCartItemAsync(Guid cartItemId);
        Task<bool> RemoveCartItem(CartItem cartItem);
        Task<CartItem> CheckIfCartContains(Guid product, Guid user);
        Task<bool> UpdateCartItem(CartItem cartItem);
    }
}
