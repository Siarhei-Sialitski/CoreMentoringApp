using System.Collections.Generic;
using CoreMentoringApp.Core.Models;

namespace CoreMentoringApp.Data
{
    public interface IDataRepository
    {

        #region Categories

        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);

        Category UpdateCategory(Category category);

        #endregion

        #region Products

        IEnumerable<Product> GetProducts(int count = 0);

        Product CreateProduct(Product product);

        Product GetProductById(int id);

        Product UpdateProduct(Product product);

        void DeleteProduct(Product product);

        #endregion

        #region Suppliers

        IEnumerable<Supplier> GetSuppliers();

        Supplier GetSupplierById(int id);

        #endregion

        int Commit();
        
    }
}
