using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class ProductController : Controller
    {

        private readonly NorthwindDbContext _dbContext;

        public ProductController(NorthwindDbContext northwindDbContext)
        {
            _dbContext = northwindDbContext;
        }

        public async  Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.ToListAsync();
            return View(products);
        }

    }
}
