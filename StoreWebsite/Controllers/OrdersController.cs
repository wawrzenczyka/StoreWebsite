using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Models;
using StoreWebsite.Services;
using StoreWebsite.Services.Users;
using StoreWebsite.Models.Orders;

namespace StoreWebsite.Controllers
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

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var orders = await _orderService.GetOrderListAsync(Guid.Parse(user.Id));

            OrderListViewModel orderList = new OrderListViewModel()
            {
                Orders = orders
            };

            return View(orderList);
        }

        public async Task<IActionResult> ChooseAddress()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }

        public async Task<IActionResult> Add(Guid addressId)
        {
            Address address = await _userService.GetAddressAsync(addressId);

            Order order = new Order() { Address = address };

            return View(order);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddConfirmed(Order newOrder)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = Guid.Parse(user.Id);

            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                Address = newOrder.Address,
                AddressId = newOrder.AddressId,
                Description = newOrder.Description,
                StatusCode = OrderStatus.New,
                UserId = userId
            };

            List<OrderDetails> orderDetails = new List<OrderDetails>();
            var cart = await _cartService.GetCartAsync(userId);

            foreach(var cartItem in cart)
            {
                orderDetails.Add(new OrderDetails()
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            bool successful = await _orderService.AddOrderAsync(order, orderDetails);
            if (!successful)
                return BadRequest(new { error = "Could not add order" });

            await _cartService.ClearCartAsync(userId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid orderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Order order = await _orderService.GetOrderAsync(orderId);

            if (Guid.Parse(user.Id) != order.UserId)
            {
                return BadRequest(new { error = "No permissions to view other users' orders" });
            }

            return View(order);
        }

        public async Task<IActionResult> Cancel(Guid orderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Order order = await _orderService.GetOrderAsync(orderId);

            if (Guid.Parse(user.Id) != order.UserId)
            {
                return BadRequest(new { error = "No permissions to view other users' orders" });
            }

            return View(order);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Cancel")]
        public async Task<IActionResult> CancelConfirmed(Order cancelledOrder)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Order order = await _orderService.GetOrderAsync(cancelledOrder.Id);

            if (order.StatusCode != OrderStatus.New)
            {
                return BadRequest(new { error = "Cannot cancel order" });
            }

            if (Guid.Parse(user.Id) != order.UserId)
            {
                return BadRequest(new { error = "No permissions to view other users' orders" });
            }

            bool successful = await _orderService.CancelOrderAsync(order);
            if (!successful)
            {
                return BadRequest(new { error = "Cannot cancel order" });
            }

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Filter(OrderFilters orderFilters)
        //{
        //    var x = DateTime.MinValue;
        //    return View(orderFilters);
        //}

        public IActionResult UpdateAddress()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(Address address)
        {
            address.Id = Guid.NewGuid();

            ModelState.Clear();
            bool validationResult = TryValidateModel(address);

            if (validationResult)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                await _userService.UpdateUserAddressAsync(user, address);

                return View("Add", address);
            }
            else
            {
                return View(address);
            }
        }

        public IActionResult SetNewAddress()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("SetNewAddress")]
        public async Task<IActionResult> SetNewAddress(Address address)
        {
            address.Id = Guid.NewGuid();

            ModelState.Clear();
            bool validationResult = TryValidateModel(address);

            if (validationResult)
            {
                bool succedeed = await _userService.AddAddressAsync(address);
                if (!succedeed)
                {
                    return BadRequest(new { error = "Could not set address" });
                }

                return View("Add", address);
            }
            else
            {
                return View(address);
            }
        }
    }
}