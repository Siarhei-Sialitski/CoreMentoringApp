using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Logging;
using CoreMentoringApp.WebSite.Options;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class ProductController : Controller
    {

        private readonly IDataRepository _dataRepository;

        private readonly ProductViewOptions _productViewOptions;

        private readonly ILogger<ProductController> _logger;

        private readonly IMapper _mapper;

        public ProductController(IDataRepository dataRepository, 
            IOptionsSnapshot<ProductViewOptions> productViewOptions, 
            ILogger<ProductController> logger,
            IMapper mapper)
        {
            _dataRepository = dataRepository;
            _productViewOptions = productViewOptions.Value;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(LogEvents.ListItems, "Get list of products.");
            return View(_dataRepository.GetProducts(_productViewOptions.Amount));
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation(LogEvents.InsertItem, "Create new product.");
            PopulateDropDownLists();
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            _logger.LogInformation(LogEvents.InsertItem, "{product} saved to database.", product);
            return RedirectToAction("Details", "Product",
                new
                {
                    id = product.ProductId
                });
        }

        public IActionResult Details(int id)
        {
            _logger.LogInformation(LogEvents.GetItem, "Get product by id={id}", id);

            var product = _dataRepository.GetProductById(id);

            if (product == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Product with {id} was not found.", id);
                return NotFound();
            }

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation(LogEvents.UpdateItem, "Update product, id={id}", id);
            var product = _dataRepository.GetProductById(id);
            if (product == null)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, "Product with {id} was not found.", id);
                return NotFound();
            }
            PopulateDropDownLists(product.CategoryId, product.SupplierId);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
