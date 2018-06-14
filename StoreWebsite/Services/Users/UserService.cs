using Microsoft.EntityFrameworkCore;
using ShopWebsite.Data;
using ShopWebsite.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Services.Users
{
    public class UserService: IUserService
    {
        readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Address> GetAddressAsync(Guid addressId)
        {
            return _context.Addresses
                .FirstAsync(a => a.Id == addressId);
        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            address.Id = Guid.NewGuid();
            await _context.AddAsync(address);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> UpdateUserAddressAsync(ApplicationUser user, Address address)
        {
            var modifiedUser = await _context.Users.FirstAsync(u => u.Id == user.Id);

            if (modifiedUser.AddressId != null)
            {
                _context.Addresses.Remove(await GetAddressAsync(modifiedUser.AddressId.Value));
            }

            address.Id = Guid.NewGuid();
            await _context.AddAsync(address);
            modifiedUser.AddressId = address.Id;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 0 || saveResult == 1 || saveResult == 2 || saveResult == 3;
        }
    }
}
