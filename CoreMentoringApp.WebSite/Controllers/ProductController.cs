using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Filters;
using CoreMentoringApp.WebSite.Options;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class ProductController : Controller
    {

        private readonly IDataRepository _dataRepository;

        private readonly ProductViewOptions _productViewOptions;
        
        private readonly IMapper _mapper;

        public ProductController(IDataRepository dataRepository, 
            IOptionsSnapshot<ProductViewOptions> productViewOptions, 
            IMapper mapper)
        {
            _dataRepository = dataRepository;
            _productViewOptions = productViewOptions.Value;
            _mapper = mapper;
        }

        [CustomizedLoggingActionFilter]
        public IActionResult Index()
        {
            return View(_dataRepository.GetProducts(_productViewOptions.Amount));
        }

        [HttpGet]
        [CustomizedLoggingActionFilter]
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomizedLoggingActionFilter]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownLists(productViewModel.CategoryId, productViewModel.SupplierId);
                return View(productViewModel);
            }

            var product = _mapper.Map<Product>(productViewModel);

            product = _dataRepository.CreateProduct(product);
            _dataRepository.Commit();

            return RedirectToAction("Details", "Product",
                new
                {
                    id = product.ProductId
                });
        }

        [CustomizedLoggingActionFilter]
        public IActionResult Details(int id)
        {
            var product = _dataRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet]
        [CustomizedLoggingActionFilter]
        public IActionResult Edit(int id)
        {
            var product = _dataRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(product.CategoryId, product.SupplierId);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomizedLoggingActionFilter]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownLists(productViewModel.CategoryId, productViewModel.SupplierId);
                return View(productViewModel);
            }

            var product = _mapper.Map<Product>(productViewModel);
            _dataRepository.UpdateProduct(product);
            _dataRepository.Commit();

            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDownLists(object categoryId = null, object supplierId = null)
        {
            PopulateCategoriesDropDownList(categoryId);
            PopulateSuppliersDropDownList(supplierId);
        }

        private void PopulateSuppliersDropDownList(object supplierId = null)
        {
            ViewBag.SupplierId = new SelectList(_dataRepository.GetSuppliers(), "SupplierId", "CompanyName", supplierId);
        }

        private void PopulateCategoriesDropDownList(object categoryId = null)
        {
            ViewBag.CategoryId = new SelectList(_dataRepository.GetCategories(), "CategoryId", "CategoryName", categoryId);
        }

    }
}
