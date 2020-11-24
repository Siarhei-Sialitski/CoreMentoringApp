using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Areas.Api.Controllers;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CoreMentoringApp.WebSite.Tests.Areas.Api.Controllers
{
    public class CategoriesControllerTests
    {

        private readonly Mock<IDataRepository> _mockDataRepository;
        
        private readonly Mock<IMapper> _mockMapper;

        public CategoriesControllerTests()
        {
            _mockDataRepository = new Mock<IDataRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void Get_ReturnsListOfCategoriesDTO()
        {
            var testCategories = GetTestCategories();
            var testCategoriesDto = GetTestCategoriesDTO();
            _mockDataRepository.Setup(repo => repo.GetCategories())
                .Returns(testCategories)
                .Verifiable();
            _mockMapper.Setup(m => m.Map<IEnumerable<CategoryDTO>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(testCategoriesDto)
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object, _mockMapper.Object);

            var result = controller.Get();
            
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CategoryDTO>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(okObjectResult.Value);
            Assert.Equal(testCategoriesDto.Count, model.Count());
            _mockDataRepository.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public void Get_ReturnsCategoryDTO_WhenIdPassed()
        {
            var testCategoryDto = GetTestCategoriesDTO().First();
            var testCategory = new Category();
            var id = 1;
            _mockDataRepository.Setup(repo => repo.GetCategoryById(id))
                .Returns(testCategory)
                .Verifiable();
            _mockMapper.Setup(m => m.Map<CategoryDTO>(testCategory))
                .Returns(testCategoryDto)
                .Verifiable();
            var controller = new CategoriesController(_mockDataRepository.Object, _mockMapper.Object);

            var result = controller.Get(id);

            var actionResult = Assert.IsType<ActionResult<CategoryDTO>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<CategoryDTO>(okObjectResult.Value);
            Assert.Equal(testCategoryDto, model);
            _mockDataRepository.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public void Put_ReceivesExistingCategoryFromRepository()
        {
            var imageDto = new ImageDTO();
            int id = 1;
            _mockDataRepository.Setup(repo => repo.GetCategoryById(id))
                .Verifiable();

            var controller = new CategoriesController(_mockDataRepository.Object, _mockMapper.Object);

            controller.Put(id, imageDto);

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

        

        private List<CategoryDTO> GetTestCategoriesDTO()
        {
            return new List<CategoryDTO>
            {
                new CategoryDTO {CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales"},
                new CategoryDTO {CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings"},
                new CategoryDTO {CategoryName = "Confections", Description = "Desserts, candies, and sweet breads"}
            };
        }

    }
}
