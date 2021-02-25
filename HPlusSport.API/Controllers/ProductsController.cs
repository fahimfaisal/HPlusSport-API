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
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
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
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameter queryParameters)  //[FromQuery] means the query comes from the url
        {
            IQueryable<Product> products = _context.Products;

            if (queryParameters.MinPrice != null && queryParameters.MinPrice != null)
            {
                products = products.Where(
                        p => p.Price >= queryParameters.MinPrice.Value &&
                             p.Price <= queryParameters.MaxPrice.Value                                        //Sets the condition to return items in  min and max price

                             );

            }

            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(p => p.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(P => P.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                products = products.Where(p => p.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()) || p.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower())); //We return the search result comparing sku and Name
            }



            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);     //We sort the items using a helper method from IQueryExtensions class
                }
            }
            products = products
                 .Skip(queryParameters.Size * (queryParameters.Page - 1))  //We skip the amount the pages according to the page
                 .Take(queryParameters.Size);                              //Take the number of products accoding to the size

            return Ok(await products.ToArrayAsync()); // We retrive all the products and return them
        }



        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);  //We retirved a single product and return it

            if (product == null)
            {
                return NotFound();                   //Getting http 404 code if no product is found
            }
            return Ok(product);
        }


        [HttpPost]

        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id }, product                                    //Returns the ID of the newly created product 


                );
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Find(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();

            }

            _context.Products.Remove(product);                             //Deleting an item using Delete method

            await _context.SaveChangesAsync();

            return product;
        }
    
    }
}
