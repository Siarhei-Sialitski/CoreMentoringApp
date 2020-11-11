using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Controllers;
using CoreMentoringApp.WebSite.Options;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CoreMentoringApp.WebSite.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IDataRepository> _mockDataRepository;

        private readonly Mock<IOptionsSnapshot<ProductViewOptions>> _mockOptions;

        private readonly Mock<IMapper> _mockMapper;


        public ProductsControllerTests()
        {
            _mockDataRepository = new Mock<IDataRepository>();
            _mockOptions = new Mock<IOptionsSnapshot<ProductViewOptions>>();
            _mockMapper = new Mock<IMapper>();
        }


        [Fact]
        public void Index_ReturnsViewResultWithListOfProducts()
        {
            _mockDataRepository.Setup(repo => repo.GetProducts(3))
                .Returns(GetTestProducts())
                .Verifiable();
            _mockOptions.Setup(opt => opt.Value)
                .Returns(GetProductViewOptions())
                .Verifiable();
            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(3, model.Count());
            _mockDataRepository.Verify();
            _mockDataRepository.Verify();
        }

        [Fact]
        public void Create_ReturnsViewWithProductViewModel()
        {
            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);
        }

        [Fact]
        public void Create_RedirectsToDetailsActionWithNewlyCreatedProductIdInRoute()
        {
            int productIdTest = 7;
            var productViewModel = new ProductViewModel();
            var product = new Product
            {
                ProductId = productIdTest
            };
            _mockMapper.Setup(m => m.Map<Product>(productViewModel))
                .Returns(product)
                .Verifiable();
            _mockDataRepository.Setup(m => m.CreateProduct(product))
                .Returns(product)
                .Verifiable();
            _mockDataRepository.Setup(m => m.Commit())
                .Verifiable();
            
            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Create(productViewModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Products", redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            Assert.Equal(productIdTest, redirectToActionResult.RouteValues["id"]);
            _mockMapper.Verify();
        }

        [Fact]
        public void Details_ReturnsViewWithProductViewModel()
        {
            int productId = 7;
            var product = new Product() {ProductId = productId};
            var productViewModel = new ProductViewModel() {ProductId = productId};
            _mockDataRepository.Setup(m => m.GetProductById(productId))
                .Returns(product)
                .Verifiable();
            _mockMapper.Setup(m => m.Map<ProductViewModel>(product))
                .Returns(productViewModel)
                .Verifiable();
            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Details(productId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var act = Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);
            Assert.Equal(productId, act.ProductId);
        }

        [Fact]
        public void Details_ReturnsNotFound_GivenIdOfNotExistedProduct()
        {
            int productId = 7;
            _mockDataRepository.Setup(m => m.GetProductById(productId))
                .Verifiable();
            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Details(productId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ReturnsViewWithProductViewModel()
        {
            int productIdTest = 7;
            var productViewModel = new ProductViewModel
            {
                ProductId = productIdTest
            };
            var product = new Product
            {
                ProductId = productIdTest
            };
            _mockDataRepository.Setup(m => m.GetProductById(productIdTest))
                .Returns(product)
                .Verifiable();
            _mockMapper.Setup(m => m.Map<ProductViewModel>(product))
                .Returns(productViewModel)
                .Verifiable();
            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Edit(productIdTest);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);
        }

        [Fact]
        public void Edit_RedirectsIndexActionWithNewlyCreatedProductIdInRoute()
        {
            int productIdTest = 7;
            var productViewModel = new ProductViewModel();
            var product = new Product
            {
                ProductId = productIdTest
            };
            _mockMapper.Setup(m => m.Map<Product>(productViewModel))
                .Returns(product)
                .Verifiable();
            _mockDataRepository.Setup(m => m.UpdateProduct(product))
                .Returns(product)
                .Verifiable();
            _mockDataRepository.Setup(m => m.Commit())
                .Verifiable();

            var controller = new ProductsController(_mockDataRepository.Object, _mockOptions.Object, _mockMapper.Object);

            var result = controller.Edit(productViewModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockMapper.Verify();
            _mockDataRepository.Verify();
        }

        private List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product {ProductId = 1, ProductName = "Chai", SupplierId = 1, CategoryId = 2, QuantityPerUnit = "10 boxes x 20 bags", UnitPrice = new decimal(18.00), UnitsInStock = 39, UnitsOnOrder = 0, ReorderLevel = 12, Discontinued = false},
                new Product {ProductId = 2, ProductName = "Chang", SupplierId = 2, CategoryId = 1, QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = new decimal(19.00), UnitsInStock = 17, UnitsOnOrder = 40, ReorderLevel = 25, Discontinued = false},
                new Product {ProductId = 3, ProductName = "Aniseed Syrup", SupplierId = 2, CategoryId = 2, QuantityPerUnit = "12 - 550 ml bottles", UnitPrice = new decimal(10.00), UnitsInStock = 13, UnitsOnOrder = 70, ReorderLevel = 25, Discontinued = true}
            };
        }

        private ProductViewOptions GetProductViewOptions()
        {
            return new ProductViewOptions{Amount = 3};
        }
    }
}
