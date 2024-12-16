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
        
        [HttpPost("StoreProduct")]
        public IActionResult StoreProduct([FromBody] Product product){
            if(product==null)
            {
                return StatusCode(500,"Badrequest");
            }
            string connectionString=_configuration.GetConnectionString("DefaultConnection");
            try{
                using(SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql="insert into Product (ProductName,Price) values (@name,@price)";
                    using(SqlCommand command=new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@name",product.ProductName);
                        command.Parameters.AddWithValue("@price",product.Price);
                        var result=command.ExecuteNonQuery();
                        if(result>0)
                        {
                            return Ok("Successfull");
                        }else
                        {
                            return StatusCode(500,"Error");
                        }
                    }
                }

            }catch(Exception e){
                return StatusCode(500,e.Message);
            }
        }
        [HttpPost("DeleteProduct")]
        public IActionResult DeleteProduct([FromBody] Product product)
        {
             if(product==null)
            {
                return StatusCode(500,"Badrequest");
            }else
            {
                Console.WriteLine("id: "+product.ProductId);
            }
            string connectionString=_configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                string sql="delete from  Ordertbl WHERE ProductId = @id";
                using(SqlCommand command=new SqlCommand(sql,connection))
                {
                    command.Parameters.AddWithValue("@id",product.ProductId);
                    command.ExecuteNonQuery();
                }
                string sql2="delete from  Product WHERE ProductId = @id";
                 using(SqlCommand command=new SqlCommand(sql2,connection))
                {
                    command.Parameters.AddWithValue("@id",product.ProductId);
                    command.ExecuteNonQuery();
                }
                return Ok("successfull");
            }
        }
    }
    public class Product{
          public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}