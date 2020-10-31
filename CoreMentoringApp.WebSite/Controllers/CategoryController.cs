using CoreMentoringApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IDataRepository _dataRepository;

        public CategoryController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            return View(_dataRepository.GetCategories());
        }

    }
}
