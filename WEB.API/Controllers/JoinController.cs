using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WEB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JoinController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public JoinController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("OrderProduct")]

        public IActionResult GetJoin()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Product.ProductId,Product.ProductName,Product.Price,Ordertbl.OrderQuantity,Product.Price*Ordertbl.OrderQuantity as Total,Ordertbl.OrderDate from Product inner join Ordertbl on Product.ProductId=Ordertbl.ProductId order by OrderDate desc;";
                    var products = new List<OrderedProduct>();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                products.Add(new OrderedProduct
                                {
                                    productId = Convert.ToInt32(reader["ProductId"]),
                                    productName = reader["ProductName"].ToString(),
                                    price = Convert.ToDecimal(reader["Price"]),
                                    quantity = Convert.ToInt32(reader["OrderQuantity"]),
                                    total = Convert.ToInt32(reader["Total"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"])

                                });
                            }

                        }
                    }
                    string grandtotal = "select sum(Product.Price*Ordertbl.OrderQuantity) as GrandTotal,avg(Product.Price*Ordertbl.OrderQuantity) as AvgTotal,sum(Ordertbl.OrderQuantity) as TotalQuantity from Product inner join Ordertbl on Product.ProductId=Ordertbl.ProductId;";
                    using (SqlCommand command = new SqlCommand(grandtotal, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            OrderSummary orderSummary = null;
                            if (reader.Read())
                            {
                                orderSummary = new OrderSummary
                                {
                                    grandttl =Convert.ToDouble(reader["GrandTotal"]),
                                    grandavg=Convert.ToDouble(reader["AvgTotal"]),
                                    grandQuantity=Convert.ToInt32(reader["TotalQuantity"])

                               };
                            }
                            return Ok(new{
                                Products = products,
                                  Totals = orderSummary
                                   });
                        }
                    }

                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
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
    public class OrderSummary
    {
        public double grandttl { get; set; }
        public double grandavg { get; set; }
        public int grandQuantity { get; set; }
    }
}