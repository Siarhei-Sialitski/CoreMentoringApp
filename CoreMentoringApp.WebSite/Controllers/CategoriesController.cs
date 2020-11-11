using System.IO;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IDataRepository _dataRepository;

        public CategoriesController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            return View(_dataRepository.GetCategories());
        }

        public IActionResult Image(int id)
        {
            var category = _dataRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return File(new MemoryStream(category.Picture), "image/jpg");
        }

        [HttpGet]
        public IActionResult UploadImage(int id)
        {
            var category = _dataRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(new UploadCategoryImageViewModel{ CategoryId = category.CategoryId, CategoryName = category.CategoryName});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadImage(UploadCategoryImageViewModel uploadCategoryImageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadCategoryImageViewModel);
            }

            var category = _dataRepository.GetCategoryById(uploadCategoryImageViewModel.CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                uploadCategoryImageViewModel.ImageFile.CopyTo(memoryStream);
                category.Picture = memoryStream.ToArray();
            }

            _dataRepository.UpdateCategory(category);
            _dataRepository.Commit();

            return RedirectToAction(nameof(Index));
        }

    }
}
