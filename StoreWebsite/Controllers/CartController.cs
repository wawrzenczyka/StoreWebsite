using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopWebsite.Models;
using ShopWebsite.Services;

namespace ShopWebsite.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, UserManager<ApplicationUser> userManager,
            ICartService cartService)
        {
            _productService = productService;
            _userManager = userManager;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = Guid.Parse(user.Id);
            var cartItems = await _cartService.GetCartAsync(userId);

            var model = new CartViewModel()
            {
                CartItems = cartItems
            };

            return View(model);
        }

        public async Task<IActionResult> Add(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddConfirmed(CartItem cartItem)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            cartItem.UserId = Guid.Parse(user.Id);

            var oldCartItem = await _cartService.CheckIfCartContains(cartItem.ProductId, cartItem.UserId);
            if (oldCartItem != null)
            {
                oldCartItem.Quantity += cartItem.Quantity;
                return await Update(oldCartItem);
            }

            bool validationResult = TryValidateModel(cartItem);

            if (validationResult)
            {
                bool result = await _cartService.AddProductToCartAsync(cartItem);
                if (!result)
                    return BadRequest(new { error = "Could not add to the cart" });
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(new { error = "Invalid quantity" });
            }
        }

        public async Task<IActionResult> Remove(Guid Id)
        {
            var cartItemViewModel = await _cartService.GetCartItemAsync(Id);

            bool validationResult = TryValidateModel(cartItemViewModel);
            if (validationResult)
            {
                bool result = await _cartService.RemoveCartItem(cartItemViewModel);
                if (!result)
                    return BadRequest(new { error = "Could not remove item" });
            }
            else
            {
                return BadRequest(new { error = "Invalid item" });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                bool result = await _cartService.UpdateCartItem(cartItem);
                if (!result)
                    return BadRequest(new { error = "Could not update item" });
            }
            else
            {
                return BadRequest(new { error = "Invalid item" });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var cartItem = await _cartService.GetCartItemAsync(Id);

            return View(cartItem);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]
        public Task<IActionResult> EditConfirmed(CartItem item)
        {
            return Update(item);
        }

        public async Task<IActionResult> Details(Guid Id)
        {
            var cartItemViewModel = await _cartService.GetCartItemAsync(Id);

            return RedirectToAction("Details", "Products", new { cartItemViewModel.Product.Id });
        }
    }
}