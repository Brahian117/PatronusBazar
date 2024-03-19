using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using PatronusBazar.BL;
using PatronusBazar.Models;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatronusBazar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ContextDB db = new ();


        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {

            if (db.CreateUser(user))
                return Ok(new { Message = "" });

            return BadRequest(new { Error = "Error, try again or contact admin" });

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {

            try
            {
                int result = db.FindUser(ref user);
                if (result == 1)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Patronus Bazaar Winter 2024 WILProject Lambton"));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                 issuer: "admin",
                 audience: "potherhead",
                 claims: new List<Claim>(),
                 expires: DateTime.UtcNow.AddMinutes(120),
                 signingCredentials: credentials);


                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        mess = "OK",
                        name = user.Name,
                        hogwartsHouse = user.HogwartsHouse
                    });
                }

                else if (result == 0)
                    return Ok(new { mess = "Password incorrect", });
                else
                    return Ok(new { mess = "User doesn't exist, you need sign in.", });

            }
            catch (Exception ex)
            {
                return BadRequest(new { mess = "Try again or contact admin" });
            }




        }


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
