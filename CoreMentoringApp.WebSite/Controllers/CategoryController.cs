using CoreMentoringApp.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class CategoryController : Controller
    {

        private readonly NorthwindDbContext _dbContext;

        public CategoryController(NorthwindDbContext northwindDbContext)
        {
            _dbContext = northwindDbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Categories);
        }

    }
}
