using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using PatronusBazar.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PatronusBazar.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (CreateUser(user))
                return Ok(new { Message = "User register" });


            return BadRequest(new { Message = "Error" });
        }

        public bool CreateUser(User user)
        {
            bool response = true;
            //testing connection
            string connectionString = "Server=localhost;User ID=root;Password=1234;Database=patronusbazaar";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Insert INTO User(Name,Phone,Email,HogwartsHouse,Username,Password) VALUES (ValName,ValPhone,ValEmail," +
                    "ValHogwartsHouse,ValUsername,ValPassword)";

                cmd.Parameters.Add("ValName", MySqlDbType.VarString).Value = user.Name;
                cmd.Parameters.Add("ValPhone", MySqlDbType.VarString).Value = user.Phone;
                cmd.Parameters.Add("ValEmail", MySqlDbType.VarString).Value = user.Email;
                cmd.Parameters.Add("ValHogwartsHouse", MySqlDbType.VarString).Value = user.HogwartsHouse;
                cmd.Parameters.Add("ValUsername", MySqlDbType.VarString).Value = user.Username;
                cmd.Parameters.Add("ValPassword", MySqlDbType.VarString).Value = user.Password;
                int x = cmd.ExecuteNonQuery();
                if (x == 0)
                    response = false;
            }
            return response;
        }

        public int FindUser(string username, string password)
        {
            int response = 1;//User and password correct
            //Testing connection
            string connectionString = "Server=localhost;User ID=root;Password=1234;Database=patronusbazaar";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select password from User where Username='" + username + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (password != reader.GetString(0)) {
                            response = 0; // Password incorrect
                        }

                    }
                    else
                    {
                        response = -1;//User incorrect
                    }
                }
            }
            return response;
        }

    }
}