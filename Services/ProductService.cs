using Newtonsoft.Json;
using slunaSVI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slunaSVI.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        public const string ProductServiceUrl = "http://10.2.7.203:8085/api/products";

        public ProductService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            var response = await _httpClient.GetAsync(ProductServiceUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductModel>>(json);
            }
            else
            {
                throw new Exception("Failed to load products");
            }
        }

        public async Task<ProductModel> GetProduct(string id)
        {
            var client = new HttpClient();
            var response = await _httpClient.GetAsync($"{ProductServiceUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(json);
            }
            else
            {
                throw new Exception("Failed to load product");
            }
        }

        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ProductServiceUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(responseJson);
            }
            else
            {
                throw new Exception("Failed to create product");
            }
        }

        public async Task<ProductModel> UpdateProduct(string id, ProductModel product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ProductServiceUrl}/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(responseJson);
            }
            else
            {
                throw new Exception("Failed to update product");
            }
        }

        public async Task DeleteProduct(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ProductServiceUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete product");
            }
        }
    }
}
