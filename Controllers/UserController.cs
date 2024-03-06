using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PatronusBazar.BL;
using PatronusBazar.Models;
using System.Data.Common;

namespace PatronusBazar.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly ContextDB db = new();

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
          
            if (db.CreateUser(user))
                return Ok(new { Message ="" });

            return BadRequest(new { Error = "Error, try again or contact admin" });

        }


    }
}