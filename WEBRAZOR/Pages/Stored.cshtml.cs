using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WEBRAZOR.Services;

namespace WEBRAZOR.Pages
{
    public class Stored : PageModel
    {
        private readonly ILogger<Stored> _logger;

        private readonly IHttpClientFactory _http;
        private readonly OrderServices _orderServices;
        public Stored(ILogger<Stored> logger, IHttpClientFactory http, OrderServices orderServices)
        {
            _logger = logger;
            _http = http;
            _orderServices = orderServices;
        }

       
       public ProductAdd products { get; set; } = new ProductAdd();
        public Product deleteid = new Product();
        public List<Product> product_list;
        public async Task OnGetAsync()
        {
            product_list = await _orderServices.GetProductsAsync();
        }

        public async Task<IActionResult> OnPostStoreDataAsync()
        {
              _logger.LogInformation("Storing Product: {Name}, Price: {Price}", products.ProductName, products.Price);

            try
            {
                products.ProductId = 1;
              
                Console.WriteLine("data:"+products);
                var client = _http.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:5095/api/Order/StoreProduct", products);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully product added");
                    products.ProductName = "";
                    products.Price = 1;
                    product_list = await _orderServices.GetProductsAsync();
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteProductAsync(int id)
        {
            try
            {
                deleteid.ProductId = id;
                var client = _http.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:5095/api/Order/DeleteProduct", deleteid);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Product successfully deleted");
                    product_list = await _orderServices.GetProductsAsync();
                }
                else
                {
                    Console.WriteLine("Failed to delete product");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToPage();
        }


    }
    public class ProductAdd
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}