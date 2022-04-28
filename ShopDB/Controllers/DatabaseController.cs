using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace ShopDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        SqlConnection conn = Connection.GetConnection();
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(ILogger<DatabaseController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Select")]
        public string Get()
        {
            return Select();
        }
        [HttpGet("Insert")]
        public string InsertGet(string name, int price, int categoryId, int manufId)
        {
            return Insert(name, price, categoryId, manufId);
        }
        string Select()
        {
            string data = string.Empty;
            try
            {
                using (SqlCommand command = new SqlCommand($"SELECT * FROM [Product]", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data += $"Name: {reader["ProductName"]}\r\nPrice: {reader["Price"]}\r\nCategory id: {reader["idCategory"]}\r\nManuf id: {reader["idManuf"]}\r\n*****\r\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return data;
        }

        string Insert(string name, int price, int catId, int manufId)
        {
            try
            {
                string query = $"INSERT INTO Product (ProductName,Price,idCategory,idManuf) VALUES (@ProductName,@Price,@idCategory,@idManuf)";
                using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                {
                    try
                    {
                        SqlParameter parameter1 = sqlCommand.Parameters.AddWithValue("@ProductName", name);
                        SqlParameter parameter2 = sqlCommand.Parameters.AddWithValue("@Price", price);
                        SqlParameter parameter3 = sqlCommand.Parameters.AddWithValue("@idCategory", catId);
                        SqlParameter parameter4 = sqlCommand.Parameters.AddWithValue("@idManuf", manufId);

                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Added 1 row!";
        }
    }
}
