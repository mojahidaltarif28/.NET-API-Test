using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WEB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderedController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderedController(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        [HttpGet]
        public IActionResult GetOrdered()
        {
            
                 string connectionString=_configuration.GetConnectionString("DefaultConnection");
            try{
                using(SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql="select * from Ordertbl";
                    using(SqlCommand command=new SqlCommand(sql,connection))
                    {
                        using(SqlDataReader reader=command.ExecuteReader())
                        {
                            var products=new List<Ordered>();
                            while(reader.Read())
                            {
                                products.Add(new Ordered{
                                    OrderId=Convert.ToInt32(reader["OrderId"]),
                                    ProductId=Convert.ToInt32(reader["ProductId"]),
                                    OrderQuantity=Convert.ToInt32(reader["OrderQuantity"]),
                                    OrderDate=Convert.ToDateTime(reader["OrderDate"])
                                });
                            }
                            return Ok(products);
                        }
                    }
                }
            }
            catch(Exception e){
                return StatusCode(500,e.Message);
            }
        }
    }
    public class Ordered{
       public int OrderId {get;set;}
       public int ProductId {get;set;}
       public int OrderQuantity {get;set;}
       public DateTime OrderDate {get;set;}
    }
}