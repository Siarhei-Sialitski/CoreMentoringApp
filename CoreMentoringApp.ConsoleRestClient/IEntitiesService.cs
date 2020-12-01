using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMentoringApp.ConsoleRestClient.Models;

namespace CoreMentoringApp.ConsoleRestClient
{
    public  interface IEntitiesService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}