using Microsoft.AspNetCore.Mvc;
using PatronusBazar.BL;
using PatronusBazar.Models;
using System;

namespace PatronusBazar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ContextDB db = new ContextDB();

    [HttpPost("/createuser")]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                // Validate user data
                if (user == null)
                {
                    return BadRequest("Invalid user data");
                }
                if (string.IsNullOrEmpty(user.Name))
                {
                    return BadRequest("User name is required");
                }
                if (string.IsNullOrEmpty(user.Email) || !IsValidEmail(user.Email))
                {
                    return BadRequest("Invalid email address");
                }
                // Add more validation rules as needed

                // Create user
                if (db.CreateUser(user))
                {
                    return Ok(new { Message = "User created successfully" });
                }
                else
                {
                    return StatusCode(500, "Failed to create user");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error creating user: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }
    
  

    [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                // Validate login request
                if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
                {
                    return BadRequest("Invalid login request");
                }

                // Authenticate user
                if (db.UserLogin(loginModel.Email, loginModel.Password))
                {
                   return Ok(new {  Message = "Login successful" });
                }
                else
                {
                    return Unauthorized("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error during user login: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    






        // Helper method to validate email address format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
