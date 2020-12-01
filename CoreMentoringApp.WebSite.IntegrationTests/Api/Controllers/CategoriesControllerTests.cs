using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using CoreMentoringApp.WebSite.IntegrationTests.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace CoreMentoringApp.WebSite.IntegrationTests.Api.Controllers
{
    public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CategoriesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointReturnSuccessAndCorrectContentTypeAndNotEmptyIEnumerableOfCategoryDTO()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/categories");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.NotEmpty(JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(await response.Content.ReadAsStringAsync()));
        }
    }
}
