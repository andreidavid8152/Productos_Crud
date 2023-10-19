using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using WebApplication1.Configurations;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ApiService : IApiService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        public ApiService(IOptions<ApiSettings> apiSettings, HttpClient httpClient)
        {
            _baseUrl = apiSettings.Value.BaseUrl;
            _httpClient = httpClient;
        }


        public async Task<List<Producto>> getProductos()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}Producto");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<List<Producto>>(content);
                return productos;
            }
            return null;
        }
    }
}
