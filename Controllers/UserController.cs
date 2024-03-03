using PatronusBazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatronusBazar.Data;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly PatronusDbContext _context;

    public UserController(PatronusDbContext context)
    {
        _context = context;
    }

    [HttpPost("Register")]
    public IActionResult RegisterUser([FromBody] User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully!");
        }
        else
        {
            return BadRequest("Invalid user data.");
        }
    }
}
