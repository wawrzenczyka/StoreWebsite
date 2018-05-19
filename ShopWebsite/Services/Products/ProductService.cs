using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Data;
using ShopWebsite.Models;
using ShopWebsite.Models.Products;

namespace ShopWebsite.Services
{
    public class ProductService : IProductService
    {
        readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(product => product.Image)
                .OrderBy(product => product.Name)
                .ToArrayAsync();
        }

        public Task<Product> GetProductAsync(Guid Id)
        {
            return _context.Products
                .Include(product => product.Image)
                .FirstAsync(p => p.Id == Id);
        }

        public async Task<bool> AddProductAsync(Product newProduct)
        {
            ProductImage image = new ProductImage();

            using (var memoryStream = new MemoryStream())
            {
                await newProduct.ImageFile.CopyToAsync(memoryStream);
                image.Image = memoryStream.ToArray();
            }

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = newProduct.Name,
                TypeCode = newProduct.TypeCode,
                Price = newProduct.Price,
                Description = newProduct.Description,
                Image = image
            };

            image.ProductId = product.Id;

            _context.Add(image);
            _context.Add(product);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 2;
        }

        public async Task<bool> RemoveProductAsync(Product product)
        {
            _context.Remove(product);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 2;
        }

        public async Task<bool> EditProductAsync(Product product)
        {
            var modifiedProduct = await _context.Products.FirstAsync(p => p.Id == product.Id);

            modifiedProduct.Name = product.Name;
            modifiedProduct.Price = product.Price;
            modifiedProduct.Description = product.Description;
            modifiedProduct.TypeCode = product.TypeCode;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> EditProductImageAsync(Product product)
        {
            var modifiedProduct = await _context.Products
                .Include(p => p.Image)
                .FirstAsync(p => p.Id == product.Id);

            using (var memoryStream = new MemoryStream())
            {
                await product.ImageFile.CopyToAsync(memoryStream);
                modifiedProduct.Image.Image = memoryStream.ToArray();
            }

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1 || saveResult == 0;
        }
    }
}
