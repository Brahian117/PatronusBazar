using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PatronusBazar.BL;
using PatronusBazar.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace PatronusBazar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        readonly ContextDB db = new ContextDB();

        [HttpPost("/createproduct")]
        public ActionResult<PatronusBazar.Models.Product> CreateProduct([FromBody] PatronusBazar.Models.Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Invalid product data");
                }

                db.CreateProduct(product);

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error creating product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("/products")]
        public ActionResult<IEnumerable<PatronusBazar.Models.Product>> GetAllProducts()
        {
            try
            {
                List<PatronusBazar.Models.Product> products = db.GetAllProducts();

                if (products.Count == 0)
                {
                    var hardcodedProduct = new PatronusBazar.Models.Product
                    {
                        Id = 1,
                        Title = "Default Product",
                        // Add other properties as needed
                    };

                    return Ok(new List<PatronusBazar.Models.Product> { hardcodedProduct });
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error retrieving products: {ex.Message}");

                return StatusCode(500, "An error occurred while fetching products.");
            }
        }

        [HttpGet("/product/{id}")]
        public ActionResult<PatronusBazar.Models.Product> GetProduct(int id)
        {
            try
            {
                PatronusBazar.Models.Product product = db.GetProductById(id);

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error retrieving product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("/updateproduct/{id}")]
        public ActionResult<PatronusBazar.Models.Product> UpdateProduct(int id, [FromBody] PatronusBazar.Models.Product updatedProduct)
        {
            try
            {
                if (updatedProduct == null)
                {
                    return BadRequest("Invalid product data");
                }

                PatronusBazar.Models.Product existingProduct = db.GetProductById(id);

                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }

                // Update properties of the existing product
                existingProduct.Title = updatedProduct.Title;
                // Update other properties as needed

                db.UpdateProduct(existingProduct);

                return Ok(existingProduct);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error updating product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("/deleteproduct/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                bool deletionSuccessful = db.DeleteProduct(id);
        
                if (deletionSuccessful)
                {
                    return Ok("Product deleted successfully");
                }
                else
                {
                    return NotFound("Product not found or deletion failed");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error deleting product: {ex.Message}");
        
                return StatusCode(500, "Internal Server Error");
            }
        }
        

        [HttpPost("/additemtocart")]
        public ActionResult AddToCart([FromBody] CartItem cartItem)
        {
            try
            {
                if (cartItem == null)
                {
                    return BadRequest("Invalid cart item data");
                }

                bool success = db.AddItemToCart(cartItem);

                if (success)
                {
                    return Ok("Item added to cart");
                }
                else
                {
                    return StatusCode(500, "Failed to add item to cart");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error adding item to cart: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }


  [HttpGet("/getcart/{userId}")]
public ActionResult<IEnumerable<CartItemDetails>> GetCart(int userId)
{
    try
    {
        List<CartItemDetails> cartItems = db.GetCartItemsWithDetails(userId);

        if (cartItems.Count == 0)
        {
            return NotFound("Cart is empty");
        }

        // Ensure that the Product property is included in the response
        var response = cartItems.Select(item => new
        {
            item.CartItemId,
            item.UserId,
            item.UserName,
            item.ProductId,
            item.Product, // Include the entire product object
            item.Quantity
        });

        return Ok(response);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error retrieving cart items: {ex.Message}");
        return StatusCode(500, "Internal Server Error");
    }
}







    }
}