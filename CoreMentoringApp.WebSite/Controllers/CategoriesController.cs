using System.IO;
using System.Threading.Tasks;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {

        private readonly IDataRepository _dataRepository;

        public CategoriesController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataRepository.GetCategoriesAsync());
        }

        public async Task<IActionResult> Image(int id)
        {
            var category = await _dataRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return File(new MemoryStream(category.Picture), "image/jpg");
        }

        [HttpGet]
        public async Task<IActionResult> UploadImage(int id)
        {
            var category = await _dataRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(new UploadCategoryImageViewModel{ CategoryId = category.CategoryId, CategoryName = category.CategoryName});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(UploadCategoryImageViewModel uploadCategoryImageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadCategoryImageViewModel);
            }

            var category = await _dataRepository.GetCategoryByIdAsync(uploadCategoryImageViewModel.CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                await uploadCategoryImageViewModel.ImageFile.CopyToAsync(memoryStream);
                category.Picture = memoryStream.ToArray();
            }

            await _dataRepository.UpdateCategoryAsync(category);
            await _dataRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
