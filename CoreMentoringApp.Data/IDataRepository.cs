using System.Collections.Generic;
using CoreMentoringApp.Core.Models;

namespace CoreMentoringApp.Data
{
    public interface IDataRepository
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Supplier> GetSuppliers();

        IEnumerable<Product> GetProducts(int count = 0);

        Product CreateProduct(Product product);

        Product GetProductById(int id);

        Product UpdateProduct(Product product);

        int Commit();
        
    }
}
