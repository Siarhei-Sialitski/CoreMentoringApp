using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoreMentoringApp.ConsoleRestClient.Models;
using Newtonsoft.Json;

namespace CoreMentoringApp.ConsoleRestClient
{
    public class RestApiEntitiesService : IEntitiesService
    {

        private const string BASE_URI = "https://localhost:44346/api/";

        private readonly HttpClient _httpClient;

        public RestApiEntitiesService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            
            var request = new HttpRequestMessage(HttpMethod.Get, $"{BASE_URI}products");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{BASE_URI}categories");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<IEnumerable<Category>>(await response.Content.ReadAsStringAsync());
        }
    }
}