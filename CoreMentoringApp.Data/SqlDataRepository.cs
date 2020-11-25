using System.Collections.Generic;
using System.Linq;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMentoringApp.Data
{
    public class SqlDataRepository : IDataRepository
    {

        private readonly NorthwindDbContext _dbContext;

        public SqlDataRepository(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Categories

        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories;
        }

        public Category UpdateCategory(Category category)
        {
            var entity = _dbContext.Categories.Attach(category);
            entity.State = EntityState.Modified;
            return category;
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.CategoryId == id);
        }

        #endregion

        #region Products

        public IEnumerable<Product> GetProducts(int count = 0)
        {
            var products = _dbContext.Products.AsQueryable();
            if (count != 0)
            {
                products = products.Take(count);
            }

            return products
                .Include(p => p.Category)
                .Include(p => p.Supplier);
        }

        public Product CreateProduct(Product product)
        {
            _dbContext.Add(product);
            return product;
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .SingleOrDefault(p => p.ProductId == id);
        }

        public Product UpdateProduct(Product product)
        {
            var entity = _dbContext.Products.Attach(product);
            entity.State = EntityState.Modified;
            return product;
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        #endregion

        #region Suppliers

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _dbContext.Suppliers;
        }

        public Supplier GetSupplierById(int id)
        {
            return _dbContext.Suppliers.SingleOrDefault(s => s.SupplierId == id);
        }

        #endregion

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        
    }
}
