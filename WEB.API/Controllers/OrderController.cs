using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WEB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        [HttpGet]
        public IActionResult GetOrder()
        {
            string connectionString=_configuration.GetConnectionString("DefaultConnection");
            try{
                using(SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql="select * from Product";
                    using(SqlCommand command=new SqlCommand(sql,connection))
                    {
                        using(SqlDataReader reader=command.ExecuteReader())
                        {
                            var products=new List<Product>();
                            while(reader.Read())
                            {
                                products.Add(new Product{
                                    ProductId=Convert.ToInt32(reader["ProductId"]),
                                    ProductName=reader["ProductName"].ToString(),
                                    Price=Convert.ToDecimal(reader["Price"])
                                });
                            }
                            return Ok(products);
                        }
                    }
                }

            }catch(Exception e)
            {
                return StatusCode(500,e.Message);

            }
        }
        
    }
    public class Product{
          public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}