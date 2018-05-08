using ShopWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Services
{
    public interface IOrderService
    {
        Task<bool> AddProductToCartAsync(CartItem newCartItem);
        Task<IEnumerable<CartItemViewModel>> GetCartAsync(Guid userId);
    }
}
