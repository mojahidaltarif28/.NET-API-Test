using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WEBRAZOR.Pages
{
    public class Join : PageModel
    {
        private readonly ILogger<Join> _logger;
        private readonly IHttpClientFactory _http;
        public Join(ILogger<Join> logger, IHttpClientFactory http)
        {
            _logger = logger;
            _http = http;
        }


        public orderdata orderdatas;
        public async Task OnGetAsync()
        {
            try
            {
                var client=_http.CreateClient();
                orderdatas = await client.GetFromJsonAsync<orderdata>("http://localhost:5095/api/Join/OrderProduct");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public class OrderedProduct
        {
            public int productId { get; set; }
            public string productName { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
            public int total { get; set; }
            public DateTime OrderDate { get; set; }
        }
        public class UnorderedProduct
        {
            public int productId { get; set; }
            public string productName { get; set; }
            public decimal price { get; set; }
        }
        public class OrderSummary
        {
            public double grandttl { get; set; }
            public double grandavg { get; set; }
            public int grandQuantity { get; set; }
        }
        public class orderdata
        {
            public List<OrderedProduct> products { get; set; }
            public OrderSummary totals { get; set; }
            public List<UnorderedProduct> unorderedproduct { get; set; }
        }
    }
}