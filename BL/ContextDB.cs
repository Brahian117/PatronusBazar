using MySql.Data.MySqlClient;
using PatronusBazar.Models;

namespace PatronusBazar.BL
{
    public class ContextDB
    {
      private readonly string connectionString = "Server=localhost;User ID=root;Password=root;Database=patronusbazaar";


    public bool CreateUser(User user)
        {
            bool response = false; // Initialize response as false
        
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO users (Name, Phone, Email, HogwartsHouse, Username, Password) VALUES (@Name, @Phone, @Email, @HogwartsHouse, @Username, @Password)";
        
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@HogwartsHouse", user.HogwartsHouse);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
        
                    int rowsAffected = cmd.ExecuteNonQuery();
        
                    if (rowsAffected > 0)
                        response = true; // Set response to true if the insert was successful
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error creating user: {ex.Message}");
                // Optionally, you can rethrow the exception here if you want it to be handled by the caller
                throw;
            }
        
            return response;
        }
        

    public bool UserLogin(string email, string password)
{
    bool isAuthenticated = false; // Initialize authentication status as false

    try
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM users WHERE Email = @Email AND Password = @Password";

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password); // Note: In a real-world scenario, passwords should be hashed and stored securely

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count > 0)
                isAuthenticated = true; // Set authentication status to true if credentials match
        }
    }
    catch (Exception ex)
    {
        // Log the exception for debugging purposes
        Console.Error.WriteLine($"Error during user login: {ex.Message}");
        // Optionally, you can rethrow the exception here if you want it to be handled by the caller
        throw;
    }

    return isAuthenticated;
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
        
                    if (product.Images != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < product.Images.Count)
                            {
                                cmd.Parameters.AddWithValue($"@image{i+1}", product.Images[i]);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue($"@image{i+1}", null);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            cmd.Parameters.AddWithValue($"@image{i+1}", null);
                        }
                    }
        
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
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Products", conn);
        
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
                                Thumbnail = Convert.ToString(reader["Thumbnail"]),
                                Images = new List<string>()
                            };
        
                            // Add image URLs to the product's image list
                            for (int i = 1; i <= 4; i++)
                            {
                                string imageFieldName = $"Image{i}";
                                if (!reader.IsDBNull(reader.GetOrdinal(imageFieldName)))
                                {
                                    string imageUrl = Convert.ToString(reader[imageFieldName]);
                                    product.Images.Add(imageUrl);
                                }
                            }
        
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
                    cmd.Parameters.AddWithValue("@productId", productId);
        
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
                                Thumbnail = Convert.ToString(reader["Thumbnail"]),
                                Images = new List<string>()
                            };
        
                            // Add image URLs to the product's image list
                            for (int i = 1; i <= 4; i++)
                            {
                                string imageFieldName = $"Image{i}";
                                if (!reader.IsDBNull(reader.GetOrdinal(imageFieldName)))
                                {
                                    string imageUrl = Convert.ToString(reader[imageFieldName]);
                                    product.Images.Add(imageUrl);
                                }
                            }
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
                    cmd.CommandText = "UPDATE Products SET Title=@Title, Description=@Description, Price=@Price, DiscountPercentage=@DiscountPercentage, Rating=@Rating, Stock=@Stock, Brand=@Brand, Category=@Category, Thumbnail=@Thumbnail, Image1=@Image1, Image2=@Image2, Image3=@Image3, Image4=@Image4 WHERE Id=@Id";
        
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
        
                    // Set image parameters
                    for (int i = 0; i < 4; i++)
                    {
                        string imageParamName = $"@Image{i + 1}";
                        string imageUrl = (i < updatedProduct.Images.Count) ? updatedProduct.Images[i] : null;
                        cmd.Parameters.AddWithValue(imageParamName, imageUrl);
                    }
        
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
        
                    // Retrieve image URLs associated with the product
                    List<string> imageUrls = new List<string>();
                    MySqlCommand getImageUrlsCmd = new MySqlCommand("SELECT Image1, Image2, Image3, Image4 FROM Products WHERE Id=@Id", conn);
                    getImageUrlsCmd.Parameters.AddWithValue("@Id", productId);
        
                    using (var reader = getImageUrlsCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                string imageUrl = Convert.ToString(reader[i]);
                                if (!string.IsNullOrEmpty(imageUrl))
                                    imageUrls.Add(imageUrl);
                            }
                        }
                    }
        
                    // Perform any necessary actions with the image URLs (e.g., delete from storage)
        
                    // Delete the product from the database
                    MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM Products WHERE Id=@Id", conn);
                    deleteCmd.Parameters.AddWithValue("@Id", productId);
        
                    int rowsAffected = deleteCmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
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
        
  public bool AddItemToCart(CartItem cartItem)
{
    bool success = false;

    try
    {
        // Check if the user exists before adding the item to the cart
        if (!UserExists(cartItem.UserId))
        {
            Console.Error.WriteLine($"Error adding item to cart: User with ID {cartItem.UserId} does not exist.");
            return false;
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO cart (product_id, user_id, quantity) VALUES (@ProductId, @UserId, @Quantity)";

            cmd.Parameters.AddWithValue("@ProductId", cartItem.ProductId);
            cmd.Parameters.AddWithValue("@UserId", cartItem.UserId);
            cmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                success = true;
        }
    }
    catch (Exception ex)
    {
        // Log the exception for debugging purposes
        Console.Error.WriteLine($"Error adding item to cart: {ex.Message}");
        // Optionally, you can rethrow the exception here if you want it to be handled by the caller
        throw;
    }

    return success;
}

// Helper method to check if the user exists
private bool UserExists(int userId)
{
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT COUNT(*) FROM users WHERE UserId = @UserId";
        cmd.Parameters.AddWithValue("@UserId", userId);

        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }
}

public List<CartItemDetails> GetCartItemsWithDetails(int userId)
{
    List<CartItemDetails> cartItemDetails = new List<CartItemDetails>();

    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        string query = @"SELECT c.cart_id, u.UserId, u.Name AS UserName, c.product_id, p.*,
                        c.quantity 
                        FROM cart c
                        INNER JOIN users u ON c.user_id = u.UserId
                        INNER JOIN products p ON c.product_id = p.id
                        WHERE c.user_id = @UserId";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@UserId", userId);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                // Create a new Product object and populate it with all product details
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Price = Convert.ToDecimal(reader["price"]),
                    DiscountPercentage = Convert.ToDouble(reader["discountpercentage"]),
                    Rating = Convert.ToDouble(reader["rating"]),
                    Stock = Convert.ToInt32(reader["stock"]),
                    Brand = reader["brand"].ToString(),
                    Category = reader["category"].ToString(),
                    Thumbnail = reader["thumbnail"].ToString(),

                    // Add other properties as needed
                };

                // Create a new CartItemDetails object and populate it with cart item and user details
                CartItemDetails cartItemDetail = new CartItemDetails
                {
                    CartItemId = Convert.ToInt32(reader["cart_id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    UserName = reader["UserName"].ToString(),
                    ProductId = Convert.ToInt32(reader["product_id"]),
                    Product = product, // Assign the product object to the Product property
                    Quantity = Convert.ToInt32(reader["quantity"])
                };

                cartItemDetails.Add(cartItemDetail);
            }
        }
    }

    return cartItemDetails;
}



public bool UpdateCartItemQuantity(int cartItemId, int newQuantity)
{
    bool success = false;

    try
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE cart 
                             SET quantity = @NewQuantity 
                             WHERE cart_id = @CartItemId";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);
            cmd.Parameters.AddWithValue("@CartItemId", cartItemId);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                success = true;
        }
    }
    catch (Exception ex)
    {
        // Log the exception for debugging purposes
        Console.Error.WriteLine($"Error updating cart item quantity: {ex.Message}");
        // Optionally, you can rethrow the exception here if you want it to be handled by the caller
        throw;
    }

    return success;
}



    }


}
