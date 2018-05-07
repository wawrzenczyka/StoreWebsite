using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Data;
using ShopWebsite.Models;

namespace ShopWebsite.Services
{
    public class ProductService : IProductService
    {
        ApplicationDbContext _context;

        public object ViewBag { get; private set; }

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToArrayAsync();
        }

        public async Task<Product> GetProductAsync(Guid Id)
        {
            return await _context.Products.FirstAsync(p => p.Id == Id);
        }

        public async Task<bool> AddProductAsync(Product newProduct)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = newProduct.Name,
                Type = newProduct.Type,
                Price = newProduct.Price,
                Description = newProduct.Description
            };

            _context.Add(product);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveProductAsync(Product product)
        {
            _context.Remove(product);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> EditProductAsync(Product product)
        {
            var modifiedProduct = await _context.Products.FirstAsync(p => p.Id == product.Id);

            modifiedProduct.Name = product.Name;
            modifiedProduct.Price = product.Price;
            modifiedProduct.Description = product.Description;
            modifiedProduct.Type = product.Type;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
