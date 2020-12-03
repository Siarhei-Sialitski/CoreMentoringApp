using System.Threading.Tasks;
using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Filters;
using CoreMentoringApp.WebSite.Options;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace CoreMentoringApp.WebSite.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private readonly IDataRepository _dataRepository;

        private readonly ProductViewOptions _productViewOptions;
        
        private readonly IMapper _mapper;

        public ProductsController(IDataRepository dataRepository, 
            IOptionsSnapshot<ProductViewOptions> productViewOptions, 
            IMapper mapper)
        {
            _dataRepository = dataRepository;
            _productViewOptions = productViewOptions.Value;
            _mapper = mapper;
        }

        [CustomizedLoggingActionFilter]
        public async Task<IActionResult> Index()
        {
            return View(await _dataRepository.GetProductsAsync(_productViewOptions.Amount));
        }

        [HttpGet]
        [CustomizedLoggingActionFilter]
        public async Task<IActionResult> Create()
        {
            await PopulateDropDownListsAsync();
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomizedLoggingActionFilter]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownListsAsync(productViewModel.CategoryId, productViewModel.SupplierId);
                return View(productViewModel);
            }

            var product = _mapper.Map<Product>(productViewModel);

            product = await _dataRepository.CreateProductAsync(product);
            await _dataRepository.CommitAsync();

            return RedirectToAction("Details", "Products",
                new
                {
                    id = product.ProductId
                });
        }

        [CustomizedLoggingActionFilter]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _dataRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet]
        [CustomizedLoggingActionFilter]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _dataRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await PopulateDropDownListsAsync(product.CategoryId, product.SupplierId);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomizedLoggingActionFilter]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownListsAsync(productViewModel.CategoryId, productViewModel.SupplierId);
                return View(productViewModel);
            }

            var product = _mapper.Map<Product>(productViewModel);
            await _dataRepository.UpdateProductAsync(product);
            await _dataRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropDownListsAsync(object categoryId = null, object supplierId = null)
        {
            await PopulateCategoriesDropDownListAsync(categoryId);
            await PopulateSuppliersDropDownListAsync(supplierId);
        }

        private async Task PopulateSuppliersDropDownListAsync(object supplierId = null)
        {
            ViewBag.SupplierId = new SelectList(await _dataRepository.GetSuppliersAsync(), "SupplierId", "CompanyName", supplierId);
        }

        private async Task PopulateCategoriesDropDownListAsync(object categoryId = null)
        {
            ViewBag.CategoryId = new SelectList(await _dataRepository.GetCategoriesAsync(), "CategoryId", "CategoryName", categoryId);
        }

    }
}
