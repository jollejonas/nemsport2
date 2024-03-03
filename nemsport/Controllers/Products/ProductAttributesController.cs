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
    public class ProductAttributesController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ProductAttributesController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ProductAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttribute>>> GetProductAttribute()
        {
            return await _context.ProductAttribute.ToListAsync();
        }

        // GET: api/ProductAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAttribute>> GetProductAttribute(int id)
        {
            var productAttribute = await _context.ProductAttribute.FindAsync(id);

            if (productAttribute == null)
            {
                return NotFound();
            }

            return productAttribute;
        }

        // PUT: api/ProductAttributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAttribute(int id, ProductAttribute productAttribute)
        {
            if (id != productAttribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(productAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttributeExists(id))
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

        // POST: api/ProductAttributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductAttribute>> PostProductAttribute(ProductAttribute productAttribute)
        {
            _context.ProductAttribute.Add(productAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductAttribute", new { id = productAttribute.Id }, productAttribute);
        }

        // DELETE: api/ProductAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttribute(int id)
        {
            var productAttribute = await _context.ProductAttribute.FindAsync(id);
            if (productAttribute == null)
            {
                return NotFound();
            }

            _context.ProductAttribute.Remove(productAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAttributeExists(int id)
        {
            return _context.ProductAttribute.Any(e => e.Id == id);
        }
    }
}
