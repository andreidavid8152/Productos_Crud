using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApplication1.Configurations;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ApiService : IApiService
    {
        // URL base de la API con la que se interactúa.
        private readonly string _baseUrl;

        // Cliente HTTP utilizado para hacer las peticiones a la API.
        private readonly HttpClient _httpClient;

        // Constructor: inicializa el URL base y el cliente HTTP.
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

        public async Task<Producto> getProducto(int id)
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}Producto/{id}");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<Producto>(content);
                return producto;
            }
            return null;
        }

        public async Task<bool> addProducto(Producto producto)
        {
            var jsonString = JsonConvert.SerializeObject(producto);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}Producto", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateProducto(int id, Producto producto)
        {
            var jsonString = JsonConvert.SerializeObject(producto);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}Producto/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteProducto(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}Producto/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
