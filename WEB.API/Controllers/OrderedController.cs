using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WEB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderedController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderedController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetOrdered()
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from Ordertbl";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var products = new List<Ordered>();
                            while (reader.Read())
                            {
                                products.Add(new Ordered
                                {
                                    OrderId = Convert.ToInt32(reader["OrderId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"])
                                });
                            }
                            return Ok(products);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Order")]
        public IActionResult AddOrder([FromBody] AddOrder order)
        {
            if (order == null)
            {
                return BadRequest("Order data is null");
            }
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "insert into Ordertbl (ProductId,OrderQuantity,OrderDate) values (@productId,@orderQuantity,@OrderDate)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@productId", order.ProductId);
                        command.Parameters.AddWithValue("@orderQuantity", order.OrderQuantity);
                        command.Parameters.AddWithValue("@OrderDate", DateTime.UtcNow);
                        int rows = command.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            return Ok("Order add Succesfully");
                        }
                        else
                        {
                            return StatusCode(500, "Failed to insert order.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
        [HttpPost("OrderDelete")]
        public IActionResult DeleteOrder([FromBody] DeleteOrder OrderId)
        {
            if (OrderId == null)
            {
                return BadRequest("Order data is null");
            }
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Delete from Ordertbl where OrderId=@orderId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@orderId", OrderId.OrderId);
                        int rows = command.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            return Ok("Order add Succesfully");
                        }
                        else
                        {
                            return StatusCode(500, "Failed to insert order.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
    public class Ordered
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public class AddOrder
    {
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
    }
    public class DeleteOrder
    {
        public int OrderId { get; set; }
    }
     public class UpdateOrder
  {
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int OrderQuantity { get; set; }
  }
}