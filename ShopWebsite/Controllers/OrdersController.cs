using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;
using ShopWebsite.Services;

namespace ShopWebsite.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public OrdersController(IProductService productService, UserManager<ApplicationUser> userManager,
            IOrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Order(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Order")]
        public async Task<IActionResult> OrderConfirmed(CartItem cartItem)
        {
            cartItem.Id = Guid.NewGuid();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            cartItem.UserId = Guid.Parse(user.Id);

            var product = await _productService.GetProductAsync(cartItem.ProductId);
            cartItem.Product = product;

            bool validationResult = TryValidateModel(cartItem);

            if (validationResult)
            {
                bool result = await _orderService.AddProductToCartAsync(cartItem);
                if (!result)
                    return BadRequest(new { error = "Could not add to the cart" });
                return RedirectToAction("Cart");
            }
            else
            {
                return View(cartItem.ProductId);
            }
        }

        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = Guid.Parse(user.Id);
            var cartItems = await _orderService.GetCartAsync(userId);

            var model = new CartViewModel()
            {
                CartItems = cartItems,
                TotalCartValue = cartItems.Sum(item => item.TotalValue)
            };

            return View(model);
        }
    }
}