using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nemsport.Data;
using nemsport.Models.ProductModels;

namespace nemsport.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSportsController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ProductSportsController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ProductSports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSport>>> GetProductSport()
        {
            return await _context.ProductSport.ToListAsync();
        }

        // GET: api/ProductSports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSport>> GetProductSport(int id)
        {
            var productSport = await _context.ProductSport.FindAsync(id);

            if (productSport == null)
            {
                return NotFound();
            }

            return productSport;
        }

        // PUT: api/ProductSports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSport(int id, ProductSport productSport)
        {
            if (id != productSport.Id)
            {
                return BadRequest();
            }

            _context.Entry(productSport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductSports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductSport>> PostProductSport(ProductSport productSport)
        {
            _context.ProductSport.Add(productSport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductSport", new { id = productSport.Id }, productSport);
        }

        // DELETE: api/ProductSports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSport(int id)
        {
            var productSport = await _context.ProductSport.FindAsync(id);
            if (productSport == null)
            {
                return NotFound();
            }

            _context.ProductSport.Remove(productSport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductSportExists(int id)
        {
            return _context.ProductSport.Any(e => e.Id == id);
        }
    }
}
