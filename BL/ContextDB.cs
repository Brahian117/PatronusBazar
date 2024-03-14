using MySql.Data.MySqlClient;
using PatronusBazar.Models;

namespace PatronusBazar.BL
{
    public class ContextDB
    {
      private readonly string connectionString = "Server=localhost;User ID=root;Password=root;Database=patronusbazaar";


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

       public (bool success, string message) CreateProduct(Product product)
{
    bool success = true;
    string message = "";

    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO patronusbazaar.products 
                                (title, description, price, discountpercentage, rating, stock, brand, category, thumbnail, image1, image2, image3, image4) 
                                VALUES 
                                (@title, @description, @price, @discountpercentage, @rating, @stock, @brand, @category, @thumbnail, @image1, @image2, @image3, @image4)";

            cmd.Parameters.AddWithValue("@title", product.Title);
            cmd.Parameters.AddWithValue("@description", product.Description);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@discountpercentage", product.DiscountPercentage);
            cmd.Parameters.AddWithValue("@rating", product.Rating);
            cmd.Parameters.AddWithValue("@stock", product.Stock);
            cmd.Parameters.AddWithValue("@brand", product.Brand);
            cmd.Parameters.AddWithValue("@category", product.Category);
            cmd.Parameters.AddWithValue("@thumbnail", product.Thumbnail);
            cmd.Parameters.AddWithValue("@image1", product.Images.Count > 0 ? product.Images[0] : null);
            cmd.Parameters.AddWithValue("@image2", product.Images.Count > 1 ? product.Images[1] : null);
            cmd.Parameters.AddWithValue("@image3", product.Images.Count > 2 ? product.Images[2] : null);
            cmd.Parameters.AddWithValue("@image4", product.Images.Count > 3 ? product.Images[3] : null);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                success = false;
                message = "Failed to create product.";
            }
            else
            {
                message = "Product created successfully.";
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, log, etc.
            Console.WriteLine(ex.Message);
            success = false;
            message = ex.Message;
        }
    }
    return (success, message);
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
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Products WHERE Id = @productId", conn);
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


        public bool UpdateProduct(Product updatedProduct)
        {
            bool response = true;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Product SET Title=@Title, Description=@Description, Price=@Price, DiscountPercentage=@DiscountPercentage, Rating=@Rating, Stock=@Stock, Brand=@Brand, Category=@Category, Thumbnail=@Thumbnail WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("@Title", updatedProduct.Title);
                    cmd.Parameters.AddWithValue("@Description", updatedProduct.Description);
                    cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", updatedProduct.DiscountPercentage);
                    cmd.Parameters.AddWithValue("@Rating", updatedProduct.Rating);
                    cmd.Parameters.AddWithValue("@Stock", updatedProduct.Stock);
                    cmd.Parameters.AddWithValue("@Brand", updatedProduct.Brand);
                    cmd.Parameters.AddWithValue("@Category", updatedProduct.Category);
                    cmd.Parameters.AddWithValue("@Thumbnail", updatedProduct.Thumbnail);
                    cmd.Parameters.AddWithValue("@Id", updatedProduct.Id);

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

       public bool DeleteProduct(int productId)
{
    bool response = true;

    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Product WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", productId);

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
    }
}
