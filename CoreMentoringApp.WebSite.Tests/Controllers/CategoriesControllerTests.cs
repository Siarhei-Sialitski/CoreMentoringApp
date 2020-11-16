using System.Collections.Generic;
using System.Linq;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Controllers;
using CoreMentoringApp.WebSite.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CoreMentoringApp.WebSite.Tests.Controllers
{
    public class CategoriesControllerTests
    {

        private readonly Mock<IDataRepository> _mockDataRepository;

        public CategoriesControllerTests()
        {
            _mockDataRepository = new Mock<IDataRepository>();
        }

        [Fact]
        public void Index_ReturnsViewResultWithListOfCategories()
        {
            _mockDataRepository.Setup(repo => repo.GetCategories())
                .Returns(GetTestCategories)
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
            _mockDataRepository.Verify();
        }

        [Fact]
        public void Image_ReturnsFileWithImageContent()
        {
            int categoryIdTest = 1;
            var category = new Category
            {
                CategoryId = categoryIdTest,
                Picture = new byte[0]
            };
            _mockDataRepository.Setup(repo => repo.GetCategoryById(categoryIdTest))
                .Returns(category)
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object);

            var result = controller.Image(categoryIdTest);

            var fileResult = Assert.IsAssignableFrom<FileResult>(result);
            Assert.Contains("image/", fileResult.ContentType);
            _mockDataRepository.Verify();
        }

        [Fact]
        public void Image_ReturnsNotFoundResult_GivenNotExistedCategoryId()
        {
            int categoryIdTest = 1;
            _mockDataRepository.Setup(repo => repo.GetCategoryById(categoryIdTest))
                .Returns(() => null)
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object);

            var result = controller.Image(categoryIdTest);

            Assert.IsType<NotFoundResult>(result);
            _mockDataRepository.Verify();
        }

        [Fact]
        public void UploadImage_ReturnsViewWithUpload()
        {
            int categoryIdTest = 1;
            string categoryNameTest = "nametest";
            var category = new Category
            {
                CategoryId = categoryIdTest,
                CategoryName = categoryNameTest,
                Picture = new byte[0]
            };
            _mockDataRepository.Setup(repo => repo.GetCategoryById(categoryIdTest))
                .Returns(category)
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object);

            var result = controller.UploadImage(categoryIdTest);

            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UploadCategoryImageViewModel>(viewResult.Model);
            Assert.Equal(categoryIdTest, model.CategoryId);
            Assert.Equal(categoryNameTest, model.CategoryName);
            _mockDataRepository.Verify();
        }

        [Fact]
        public void UploadImage_RedirectsToIndexAction()
        {
            int categoryIdTest = 1;
            var category = new Category
            {
                CategoryId = categoryIdTest,
                Picture = new byte[0]
            };
            var mockFile = new Mock<IFormFile>();
            var categoryViewModel = new UploadCategoryImageViewModel()
            {
                CategoryId = categoryIdTest,
                ImageFile = mockFile.Object
            };
            _mockDataRepository.Setup(repo => repo.GetCategoryById(categoryIdTest))
                .Returns(category)
                .Verifiable();
            _mockDataRepository.Setup(m => m.UpdateCategory(category))
                .Verifiable();
            _mockDataRepository.Setup(m => m.Commit())
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object);

            var result = controller.UploadImage(categoryViewModel);

            var redirectToActionResultResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResultResult.ActionName);
            _mockDataRepository.Verify();
        }

        private List<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category {CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales"},
                new Category {CategoryId = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings"},
                new Category {CategoryId = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads"}
            };
        }
    }
}
