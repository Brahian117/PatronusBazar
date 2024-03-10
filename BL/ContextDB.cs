using MySql.Data.MySqlClient;
using PatronusBazar.Models;

namespace PatronusBazar.BL
{
    public class ContextDB
    {
      private readonly string connectionString = "Server=localhost;User ID=root;Password=1234;Database=patronusbazaar";


        public bool CreateUser(User user)
        {
            bool response = true;
            // string connectionString = "Server=localhost;User ID=root;Password=1234;Database=patronusbazaar";

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
            // string connectionString = "Server=localhost;User ID=root;Password=1234;Database=patronusbazaar";

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

         public bool CreateProduct(Product product)
        {
            bool response = true;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Product(Title, Description, Price, DiscountPercentage, Rating, Stock, Brand, Category, Thumbnail) VALUES (?,?,?,?,?,?,?,?,?)";

                    cmd.Parameters.AddWithValue("param1", product.Title);
                    cmd.Parameters.AddWithValue("param2", product.Description);
                    cmd.Parameters.AddWithValue("param3", product.Price);
                    cmd.Parameters.AddWithValue("param4", product.DiscountPercentage);
                    cmd.Parameters.AddWithValue("param5", product.Rating);
                    cmd.Parameters.AddWithValue("param6", product.Stock);
                    cmd.Parameters.AddWithValue("param7", product.Brand);
                    cmd.Parameters.AddWithValue("param8", product.Category);
                    cmd.Parameters.AddWithValue("param9", product.Thumbnail);

                    int x = cmd.ExecuteNonQuery();
                    if (x == 0)
                        response = false;
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log, etc.
                    Console.WriteLine(ex.Message);
                    response = false;
                }
            }
            return response;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Product", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = Convert.ToString(reader["Title"]),
                                Description = Convert.ToString(reader["Description"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                DiscountPercentage = Convert.ToDouble(reader["DiscountPercentage"]),
                                Rating = Convert.ToDouble(reader["Rating"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Brand = Convert.ToString(reader["Brand"]),
                                Category = Convert.ToString(reader["Category"]),
                                Thumbnail = Convert.ToString(reader["Thumbnail"])
                            };

                            products.Add(product);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log, etc.
                    Console.WriteLine(ex.Message);
                }
            }

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Product WHERE Id = @productId", conn);
                    cmd.Parameters.AddWithValue("productId", productId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = Convert.ToString(reader["Title"]),
                                Description = Convert.ToString(reader["Description"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                DiscountPercentage = Convert.ToDouble(reader["DiscountPercentage"]),
                                Rating = Convert.ToDouble(reader["Rating"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Brand = Convert.ToString(reader["Brand"]),
                                Category = Convert.ToString(reader["Category"]),
                                Thumbnail = Convert.ToString(reader["Thumbnail"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log, etc.
                    Console.WriteLine(ex.Message);
                }
            }

            return product;
        }
    }
}
