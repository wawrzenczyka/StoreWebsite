using StoreWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebsite.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(Guid Id);
        Task<bool> AddProductAsync(Product newProduct);
        Task<bool> RemoveProductAsync(Product product);
        Task<bool> EditProductAsync(Product product);
        Task<bool> EditProductImageAsync(Product product);
    }
}
