using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;
using ShopWebsite.Services;

namespace ShopWebsite.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();

            var model = new ProductListViewModel()
            {
                Products = products
            };

            return View(model);
        }

        [Authorize(Policy = "Management")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "Management")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                bool result = await _productService.AddProductAsync(product);
                if (!result)
                    return BadRequest(new { error = "Could not add item" });
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [Authorize(Policy = "Management")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }

        [Authorize(Policy = "Management")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);

            if (ModelState.IsValid)
            {
                bool result = await _productService.RemoveProductAsync(product);
                if (!result)
                    return BadRequest(new { error = "Could not add item" });
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [Authorize(Policy = "Management")]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }

        [Authorize(Policy = "Management")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(Product product)
        {
            if (ModelState.IsValid)
            {
                bool result = await _productService.EditProductAsync(product);
                if (!result)
                    return BadRequest(new { error = "Could not add item" });
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public async Task<IActionResult> Details(Guid Id)
        {
            var product = await _productService.GetProductAsync(Id);
            return View(product);
        }
    }
}