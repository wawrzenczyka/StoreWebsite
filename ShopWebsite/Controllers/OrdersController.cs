using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;
using ShopWebsite.Services;
using ShopWebsite.Services.Users;

namespace ShopWebsite.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IProductService productService, UserManager<ApplicationUser> userManager,
            ICartService cartService, IOrderService orderService, IUserService userService)
        {
            _productService = productService;
            _userManager = userManager;
            _cartService = cartService;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChooseAddress()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user);
        }

        public async Task<IActionResult> Add(Guid addressId)
        {
            Address address = await _userService.GetAddressAsync(addressId);
            return View(address);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddConfirmed(Address address)
        {
            //Order order = new Order()
            //{
            //    Id = Guid.NewGuid(),
            //    OrderDate = DateTime.Now,
            //    Address = address,
            //    Description = ,

            //}

            //return View(address);
            return View();
        }

        public IActionResult UpdateAddress()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(Address address)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _userService.UpdateUserAddressAsync(user, address);

            return View("Add", address);
        }

        public IActionResult SetNewAddress()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("SetNewAddress")]
        public async Task<IActionResult> SetNewAddress(Address address)
        {
            bool succedeed = await _userService.AddAddressAsync(address);
            if (!succedeed)
            {
                return BadRequest(new { error = "Could not set address" });
            }

            return View("Add", address);
        }
    }
}