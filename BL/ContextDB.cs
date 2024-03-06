using MySql.Data.MySqlClient;
using PatronusBazar.Models;

namespace PatronusBazar.BL
{
    public class ContextDB
    {

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
                cmd.CommandText = "Insert INTO User(Name,Phone,Email,HogwartsHouse,Username,Password) VALUES (?,?,?,?,?,?)";

                cmd.Parameters.AddWithValue("param1", user.Name);
                cmd.Parameters.AddWithValue("param2", user.Phone);
                cmd.Parameters.AddWithValue("param3", user.Email);
                cmd.Parameters.AddWithValue("param4", user.HogwartsHouse);
                cmd.Parameters.AddWithValue("param5", user.Username);
                cmd.Parameters.AddWithValue("param6", user.Password);
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
                        if (password != reader.GetString(0))
                        {
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
