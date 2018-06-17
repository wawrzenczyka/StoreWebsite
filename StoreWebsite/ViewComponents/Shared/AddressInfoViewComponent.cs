using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Models;
using StoreWebsite.Services;
using StoreWebsite.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.ViewComponents.Shared
{
    public class AddressInfoViewComponent: ViewComponent
    {
        readonly IUserService _userService;

        public AddressInfoViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid addressId)
        {
            Address address = await _userService.GetAddressAsync(addressId);
            return await Task.Run(() => View(address));
        }
    }
}
