using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var entity = _dbContext.Categories.Attach(category);
            entity.State = EntityState.Modified;
            return category;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories
                .Include(c => c.Products)
                .SingleOrDefaultAsync(c => c.CategoryId == id);
        }

        #endregion

        #region Products

        public async Task<IEnumerable<Product>> GetProductsAsync(int count = 0)
        {
            var products = _dbContext.Products.AsQueryable();
            if (count != 0)
            {
                products = products.Take(count);
            }

            return await products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.AddAsync(product);
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .SingleOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var entity = _dbContext.Products.Attach(product);
            entity.State = EntityState.Modified;
            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        #endregion

        #region Suppliers

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _dbContext.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _dbContext.Suppliers.SingleOrDefaultAsync(s => s.SupplierId == id);
        }

        #endregion

        public async  Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
    }
}
