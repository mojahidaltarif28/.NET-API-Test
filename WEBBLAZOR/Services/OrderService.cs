using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WEBBLAZOR.Services
{
   public class OrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Order");
                var jsonData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
                    return products;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in getproduct:{e.Message}");
            }
            return new List<Product>();
        }
        public async Task<List<Ordered>> GetOrderedsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Ordered");
                var jsonData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var ordered = JsonConvert.DeserializeObject<List<Ordered>>(jsonData);
                    return ordered;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in getOrdered:{e.Message}");
            }
            return new List<Ordered>();
        }
    }
    public class Product
    {
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
    public class Ordered
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}