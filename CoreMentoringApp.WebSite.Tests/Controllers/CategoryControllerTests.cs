using System.Collections.Generic;
using System.Linq;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CoreMentoringApp.WebSite.Tests.Controllers
{
    public class CategoryControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResultWithListOfCategories()
        {
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.GetCategories())
                .Returns(GetTestCategories)
                .Verifiable();
            var controller = new CategoryController(mockRepo.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
            mockRepo.Verify();
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
