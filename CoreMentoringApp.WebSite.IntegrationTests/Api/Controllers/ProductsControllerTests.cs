using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using CoreMentoringApp.WebSite.IntegrationTests.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace CoreMentoringApp.WebSite.IntegrationTests.Api.Controllers
{
    public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointReturnSuccessAndCorrectContentTypeAndNotEmptyIEnumerableOfProductDTO()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.NotEmpty(JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(await response.Content.ReadAsStringAsync()));
        }

        [Fact]
        public async Task Post_EndpointAddNewProductToDbContextAndReturnSuccessAndCorrectContentTypeAndLocationToAddedProduct()
        {
            //arrange
            var client = _factory.CreateClient();
            var newProductDto = new ProductDTO
            {
                ProductName = "Coffee",
                SupplierId = 2,
                CategoryId = 2,
                QuantityPerUnit = "1 kg",
                UnitPrice = new decimal(320.00),
                UnitsInStock = 17,
                UnitsOnOrder = 41,
                ReorderLevel = 33,
                Discontinued = true
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(newProductDto), Encoding.UTF8, "application/json");

            //act
            var response = await client.PostAsync("/api/products", stringContent);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.NotNull(JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync())); 
            var location = response.Headers.Location;
            Assert.StartsWith("/api/Products/", location.ToString());

            //act
            var getResponse = await client.GetAsync(location);

            //assert
            Assert.Equal("application/json; charset=utf-8", getResponse.Content.Headers.ContentType.ToString());
            var addedProductDto = Assert.IsAssignableFrom<ProductDTO>(
                JsonConvert.DeserializeObject<ProductDTO>(await getResponse.Content.ReadAsStringAsync()));
            Assert.Equal(JsonConvert.SerializeObject(newProductDto), JsonConvert.SerializeObject(addedProductDto));
        }
    }
}
