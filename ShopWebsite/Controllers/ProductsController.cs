using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;
using ShopWebsite.Services;

namespace ShopWebsite.Controllers
{
    public class ProductsController : Controller
    {
        IProductService productService;

        public ProductsController(IProductService _productService)
        {
            productService = _productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync();

            var model = new ProductViewModel()
            {
                Products = products
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                bool result = await productService.AddProductAsync(product);
                if (!result)
                    return BadRequest(new { error = "Could not add item" });
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var product = await productService.GetProductAsync(Id);
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            var product = await productService.GetProductAsync(Id);

            if (ModelState.IsValid)
            {
                bool result = await productService.RemoveProductAsync(product);
                if (!result)
                    return BadRequest(new { error = "Could not add item" });
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var product = await productService.GetProductAsync(Id);
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(Product product)
        {
            if (ModelState.IsValid)
            {
                bool result = await productService.EditProductAsync(product);
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
            var product = await productService.GetProductAsync(Id);
            return View(product);
        }
    }
}