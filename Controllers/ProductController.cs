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
        if (product == null)
        {
            return BadRequest();
        }

        db.CreateProduct(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpGet("/products")]
    public ActionResult<IEnumerable<PatronusBazar.Models.Product>> GetAllProducts()
    {
        List<PatronusBazar.Models.Product> products = db.GetAllProducts();

        if (products.Count == 0)
        {
            return NotFound();
        }

        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<PatronusBazar.Models.Product> GetProduct(int id)
    {
        PatronusBazar.Models.Product product = db.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
}
