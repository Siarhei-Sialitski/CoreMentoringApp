using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMentoringApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMentoringApp.Data
{
    public class InMemoryDataRepository : IDataRepository
    {

        private readonly List<Category> _categories;

        private readonly List<Product> _products;

        private readonly List<Supplier> _suppliers;

        public InMemoryDataRepository()
        {
            _products = new List<Product>
            {
                new Product {ProductId = 1, ProductName = "Chai", SupplierId = 1, CategoryId = 2, QuantityPerUnit = "10 boxes x 20 bags", UnitPrice = new decimal(18.00), UnitsInStock = 39, UnitsOnOrder = 0, ReorderLevel = 12, Discontinued = false},
                new Product {ProductId = 2, ProductName = "Chang", SupplierId = 2, CategoryId = 1, QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = new decimal(19.00), UnitsInStock = 17, UnitsOnOrder = 40, ReorderLevel = 25, Discontinued = false},
                new Product {ProductId = 3, ProductName = "Aniseed Syrup", SupplierId = 2, CategoryId = 2, QuantityPerUnit = "12 - 550 ml bottles", UnitPrice = new decimal(10.00), UnitsInStock = 13, UnitsOnOrder = 70, ReorderLevel = 25, Discontinued = true}
            };
            
            _categories = new List<Category>
            {
                new Category {CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales", Picture = new byte[0]},
                new Category {CategoryId = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings", Picture = new byte[0]},
                new Category {CategoryId = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads", Picture = new byte[0]}
            };

            _suppliers = new List<Supplier>
            {
                new Supplier {SupplierId = 1, CompanyName = "Exotic Liquids"},
                new Supplier {SupplierId = 2, CompanyName = "New Orleans Cajun Delights"}
            };

            BindRelatedEntities();
        }

        #region Categories

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categories.AsQueryable().ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var categoryUpd = await _categories.AsQueryable().SingleOrDefaultAsync(c => c.CategoryId == category.CategoryId);
            if (categoryUpd != null)
            {
                categoryUpd.Picture = category.Picture;
                categoryUpd.CategoryName = category.CategoryName;
                categoryUpd.Description = category.Description;
            }

            return categoryUpd;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categories.AsQueryable().SingleOrDefaultAsync(c => c.CategoryId == id);
        }

        #endregion

        #region Products

        public async Task<IEnumerable<Product>> GetProductsAsync(int count = 0)
        {
            var products = _products.AsQueryable();
            if (count != 0)
            {
                products = products.Take(count);
            }

            return await products.ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _products.Add(product);
            product.ProductId = _products.Max(p => p.ProductId) + 1;
            BindRelatedEntities();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _products.AsQueryable().SingleOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var productUpd = await _products.AsQueryable().SingleOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (productUpd != null)
            {
                productUpd.CategoryId = product.CategoryId;
                productUpd.SupplierId = product.SupplierId;
                productUpd.ProductName = product.ProductName;
                productUpd.Discontinued = product.Discontinued;
                productUpd.QuantityPerUnit = product.QuantityPerUnit;
                productUpd.ReorderLevel = product.ReorderLevel;
                productUpd.UnitPrice = product.UnitPrice;
                productUpd.UnitsInStock = product.UnitsInStock;
                productUpd.UnitsOnOrder = product.UnitsOnOrder;
            }

            BindRelatedEntities();
            return productUpd;
        }

        public async Task DeleteProductAsync(Product product)
        {
            _products.Remove(product);
        }

        #endregion

        #region Suppliers

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _suppliers.AsQueryable().ToListAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _suppliers.AsQueryable().SingleOrDefaultAsync(s => s.SupplierId == id);
        }

        #endregion

        public async Task<int> CommitAsync()
        {
            return 0;
        }

        private void BindRelatedEntities()
        {
            foreach (var product in _products)
            {
                product.Category = _categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                product.Supplier = _suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);
            }

            foreach (var category in _categories)
            {
                category.Products = new List<Product>(_products.Where(p => p.CategoryId == category.CategoryId));
            }
        }
        
    }
}
