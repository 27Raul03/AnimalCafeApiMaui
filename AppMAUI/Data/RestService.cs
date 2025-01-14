using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AppMAUI.Models;

namespace AppMAUI.Data
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://localhost:7144/api/Products"; // Replace with your API URL

        public RestService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(content) ?? new List<Product>();
            }
            return new List<Product>();
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Product>();
            }
            return null;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("products", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"products/{product.ID}", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }



        // *** CRUD pentru animale ***
        public async Task<List<Animal>> GetAnimalsAsync()
        {
            var response = await _httpClient.GetAsync("animals");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Animal>>(content) ?? new List<Animal>();
            }
            return new List<Animal>();
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
     {
         var response = await _httpClient.GetAsync($"animals/{id}");
         if (response.IsSuccessStatusCode)
         {
             return await response.Content.ReadFromJsonAsync<Animal>();
         }
         return null;
     }

        public async Task<bool> AddAnimalAsync(Animal animal)
        {
            var response = await _httpClient.PostAsJsonAsync("animals", animal);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAnimalAsync(Animal animal)
        {
            var response = await _httpClient.PutAsJsonAsync($"animals/{animal.ID}", animal);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"animals/{id}");
            return response.IsSuccessStatusCode;
        }

        // *** CRUD pentru clienți ***
        public async Task<List<Client>> GetClientsAsync()
        {
            var response = await _httpClient.GetAsync("clients");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Client>>(content) ?? new List<Client>();
            }
            return new List<Client>();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"clients/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Client>();
            }
            return null;
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("clients", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            var response = await _httpClient.PutAsJsonAsync($"clients/{client.ID}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"clients/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
