using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMentoringApp.Core.Models;

namespace CoreMentoringApp.Data
{
    public interface IDataRepository
    {

        #region Categories

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        Task<Category> UpdateCategoryAsync(Category category);

        #endregion

        #region Products

        Task<IEnumerable<Product>> GetProductsAsync(int count = 0);

        Task<Product> CreateProductAsync(Product product);

        Task<Product> GetProductByIdAsync(int id);

        Task<Product> UpdateProductAsync(Product product);

        Task DeleteProductAsync(Product product);

        #endregion

        #region Suppliers

        Task<IEnumerable<Supplier>> GetSuppliersAsync();

        Task<Supplier> GetSupplierByIdAsync(int id);

        #endregion

        Task<int> CommitAsync();
        
    }
}
