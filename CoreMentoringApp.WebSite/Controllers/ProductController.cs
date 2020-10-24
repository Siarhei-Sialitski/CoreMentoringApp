﻿using System.Linq;
using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Models;
using CoreMentoringApp.WebSite.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class ProductController : Controller
    {

        private readonly NorthwindDbContext _dbContext;

        private readonly ProductViewOptions _productViewOptions;

        public ProductController(NorthwindDbContext northwindDbContext, IOptionsSnapshot<ProductViewOptions> productViewOptions)
        {
            _dbContext = northwindDbContext;
            _productViewOptions = productViewOptions.Value;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.AsQueryable();
            if (_productViewOptions.Amount != 0)
            {
                products = products.Take(_productViewOptions.Amount);
            }
            
            return View(products
                .Include(p => p.Category)
                .Include(p => p.Supplier));
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,SupplierId,ProductName,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")]Product product)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownLists(product.CategoryId, product.SupplierId);
                return View(product);
            }

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", "Product",
                new
                {
                    id = product.ProductId
                });
        }

        public IActionResult Details(int id)
        {
            var product = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(product.CategoryId, product.SupplierId);
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var productToUpdate = _dbContext.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);
            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "",
                p => p.CategoryId, 
                p => p.SupplierId, 
                p => p.ProductName, 
                p => p.QuantityPerUnit, 
                p => p.UnitPrice, 
                p => p.UnitsInStock, 
                p => p.UnitsOnOrder, 
                p => p.ReorderLevel, 
                p => p.Discontinued))
            {
                try
                {
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }

            PopulateDropDownLists(productToUpdate?.CategoryId, productToUpdate?.SupplierId);
            return View(productToUpdate);
        }

        private void PopulateDropDownLists(object categoryId = null, object supplierId = null)
        {
            PopulateCategoriesDropDownList(categoryId);
            PopulateSuppliersDropDownList(supplierId);
        }

        private void PopulateSuppliersDropDownList(object supplierId = null)
        {
            ViewBag.SupplierId = new SelectList(_dbContext.Suppliers.AsNoTracking(), "SupplierId", "CompanyName", supplierId);
        }

        private void PopulateCategoriesDropDownList(object categoryId = null)
        {
            ViewBag.CategoryId = new SelectList(_dbContext.Categories.AsNoTracking(), "CategoryId", "CategoryName", categoryId);
        }

    }
}
