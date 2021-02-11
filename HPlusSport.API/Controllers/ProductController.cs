using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSport.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductController(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated(); //We create the database and fill it with data.


        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_context.Products.ToArray()); // We retrive all the products and return them
        }

        [HttpGet("{id}")]

        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);  //We retirved a single product and return it
            return Ok(product);
        }
    }
}
