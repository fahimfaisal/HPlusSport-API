using HPlusSport.API.Classes;
using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //[HttpGet]
        //public IActionResult GetAllProducts()
        //{
        //    return Ok(_context.Products.ToArray()); // We retrive all the products and return them
        //}

        //[HttpGet("{id:int}")]        

        //public IActionResult GetProduct(int id)
        //{
        //    var product = _context.Products.Find(id);  //We retirved a single product and return it

        //    if (product == null)
        //    {
        //        return NotFound();                   //Getting http 404 code if no product is found
        //    }
        //    return Ok(product);
        //}

        //Creating Asynchronous Actions

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] QueryParameters queryParameters)  //[FromQuery] means the query comes from the url
        {
            IQueryable<Product> products = _context.Products;

            products = products
                 .Skip(queryParameters.Size * (queryParameters.Page - 1))  //We skip the amount the pages according to the page
                 .Take(queryParameters.Size);                              //Take the number of products accoding to the size

            return Ok(await products.ToArrayAsync()); // We retrive all the products and return them
        }

        [HttpGet("{id:int}")]

        public async  Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);  //We retirved a single product and return it

            if (product == null)
            {
                return NotFound();                   //Getting http 404 code if no product is found
            }
            return Ok(product);
        }
    }
}
