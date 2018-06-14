using ShopWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Services.Users
{
    public interface IUserService
    {
        Task<bool> UpdateUserAddressAsync(ApplicationUser user, Address address);
        Task<Address> GetAddressAsync(Guid addressId);
        Task<bool> AddAddressAsync(Address address);
    }
}
