using System.Linq;
using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Models;
using CoreMentoringApp.WebSite.Options;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var products = _dbContext.Products.AsQueryable();
            if (_productViewOptions.Amount != 0)
            {
                products = products.Take(_productViewOptions.Amount);
            }
            
            return View(await products.Include(x => x.Category).ToListAsync());
        }

    }
}
