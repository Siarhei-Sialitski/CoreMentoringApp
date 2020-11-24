using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Areas.Api.Controllers;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace CoreMentoringApp.WebSite.Tests.Areas.Api.Controllers
{
    public class ProductsControllerTests
    {

        private readonly Mock<IDataRepository> _mockDataRepository;
        
        private readonly Mock<IMapper> _mockMapper;

        private readonly Mock<LinkGenerator> _mockLinkGenerator;

        public ProductsControllerTests()
        {
            _mockDataRepository = new Mock<IDataRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLinkGenerator = new Mock<LinkGenerator>();
        }

        [Fact]
        public void Get_ReturnsListOfProductDTO()
        {
            var testProducts = GetTestProducts();
            var testProductsDto = GetTestProductsDTO();
            _mockDataRepository.Setup(repo => repo.GetProducts(It.IsAny<int>()))
                .Returns(testProducts)
                .Verifiable();
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(testProductsDto)
                .Verifiable();
            var controller = new ProductsController(_mockDataRepository.Object, _mockMapper.Object, _mockLinkGenerator.Object);

            var result = controller.Get();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductDTO>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(okObjectResult.Value);
            Assert.Equal(testProductsDto.Count, model.Count());
            _mockDataRepository.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public void Get_ReturnsProductDTO_WhenIdPassed()
        {
            var testProduct = GetTestProducts().First();
            var testProductDto = GetTestProductsDTO().First();
            var id = 1;
            _mockDataRepository.Setup(repo => repo.GetProductById(id))
                .Returns(testProduct)
                .Verifiable();
            _mockMapper.Setup(m => m.Map<ProductDTO>(testProduct))
                .Returns(testProductDto)
                .Verifiable();
            var controller = new ProductsController(_mockDataRepository.Object, _mockMapper.Object, _mockLinkGenerator.Object);

            var result = controller.Get(id);
            
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<ProductDTO>(okObjectResult.Value);
            Assert.Equal(testProductDto, model);
            _mockDataRepository.Verify();
            _mockMapper.Verify();
        }

        [Fact]
        public void Post_ChecksSupplierAndCategoryExisting()
        {
            var testProductDto = GetTestProductsDTO().First();
            _mockDataRepository.Setup(repo => repo.GetSupplierById(testProductDto.SupplierId.Value))
                .Verifiable();
            _mockDataRepository.Setup(repo => repo.GetCategoryById(testProductDto.CategoryId.Value))
                .Returns(new Category())
                .Verifiable();

            var controller = new ProductsController(_mockDataRepository.Object, _mockMapper.Object, _mockLinkGenerator.Object);

            controller.Post(testProductDto);
            
            _mockDataRepository.Verify();
        }

        [Fact]
        public void Put_ReceivesExistingProductFromRepository()
        {
            var testProductDto = GetTestProductsDTO().First();
            int id = 1;
            _mockDataRepository.Setup(repo => repo.GetProductById(id))
                .Verifiable();

            var controller = new ProductsController(_mockDataRepository.Object, _mockMapper.Object, _mockLinkGenerator.Object);

            controller.Put(id, testProductDto);

            _mockDataRepository.Verify();
        }

        [Fact]
        public void Delete_ReceivesExistingProductFromRepositoryAndDelete()
        {
            var testProductDto = GetTestProductsDTO().First();
            var testProduct = GetTestProducts().First();
            int id = 1;
            _mockDataRepository.Setup(repo => repo.GetProductById(id))
                .Returns(testProduct)
                .Verifiable();
            _mockDataRepository.Setup(repo => repo.DeleteProduct(testProduct))
                .Verifiable();

            var controller = new ProductsController(_mockDataRepository.Object, _mockMapper.Object, _mockLinkGenerator.Object);

            controller.Delete(id);

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

        private List<ProductDTO> GetTestProductsDTO()
        {
            return new List<ProductDTO>
            {
                new ProductDTO { ProductName = "Chai", SupplierId = 1, CategoryId = 2, QuantityPerUnit = "10 boxes x 20 bags", UnitPrice = new decimal(18.00), UnitsInStock = 39, UnitsOnOrder = 0, ReorderLevel = 12, Discontinued = false},
                new ProductDTO { ProductName = "Chang", SupplierId = 2, CategoryId = 1, QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = new decimal(19.00), UnitsInStock = 17, UnitsOnOrder = 40, ReorderLevel = 25, Discontinued = false},
                new ProductDTO { ProductName = "Aniseed Syrup", SupplierId = 2, CategoryId = 2, QuantityPerUnit = "12 - 550 ml bottles", UnitPrice = new decimal(10.00), UnitsInStock = 13, UnitsOnOrder = 70, ReorderLevel = 25, Discontinued = true}
            };
        }

    }
}
