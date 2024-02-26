using Microsoft.AspNetCore.Mvc;
using PatronusBazar.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterUser([FromBody] User user)
    {
  
        return Ok(user);
    }
}