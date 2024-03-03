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
    public class ProductVariationsController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ProductVariationsController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ProductVariations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVariation>>> GetProductVariation()
        {
            return await _context.ProductVariation.ToListAsync();
        }

        // GET: api/ProductVariations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVariation>> GetProductVariation(int id)
        {
            var productVariation = await _context.ProductVariation.FindAsync(id);

            if (productVariation == null)
            {
                return NotFound();
            }

            return productVariation;
        }

        // PUT: api/ProductVariations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductVariation(int id, ProductVariation productVariation)
        {
            if (id != productVariation.Id)
            {
                return BadRequest();
            }

            _context.Entry(productVariation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductVariationExists(id))
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

        // POST: api/ProductVariations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductVariation>> PostProductVariation(ProductVariation productVariation)
        {
            _context.ProductVariation.Add(productVariation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductVariation", new { id = productVariation.Id }, productVariation);
        }

        // DELETE: api/ProductVariations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductVariation(int id)
        {
            var productVariation = await _context.ProductVariation.FindAsync(id);
            if (productVariation == null)
            {
                return NotFound();
            }

            _context.ProductVariation.Remove(productVariation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductVariationExists(int id)
        {
            return _context.ProductVariation.Any(e => e.Id == id);
        }
    }
}
