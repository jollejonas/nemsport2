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
    public class ProductAttributeValuesController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ProductAttributeValuesController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ProductAttributeValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttributeValue>>> GetProductAttributeValue()
        {
            return await _context.ProductAttributeValue.ToListAsync();
        }

        // GET: api/ProductAttributeValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAttributeValue>> GetProductAttributeValue(int id)
        {
            var productAttributeValue = await _context.ProductAttributeValue.FindAsync(id);

            if (productAttributeValue == null)
            {
                return NotFound();
            }

            return productAttributeValue;
        }

        // PUT: api/ProductAttributeValues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAttributeValue(int id, ProductAttributeValue productAttributeValue)
        {
            if (id != productAttributeValue.Id)
            {
                return BadRequest();
            }

            _context.Entry(productAttributeValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttributeValueExists(id))
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

        // POST: api/ProductAttributeValues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductAttributeValue>> PostProductAttributeValue(ProductAttributeValue productAttributeValue)
        {
            _context.ProductAttributeValue.Add(productAttributeValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductAttributeValue", new { id = productAttributeValue.Id }, productAttributeValue);
        }

        // DELETE: api/ProductAttributeValues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttributeValue(int id)
        {
            var productAttributeValue = await _context.ProductAttributeValue.FindAsync(id);
            if (productAttributeValue == null)
            {
                return NotFound();
            }

            _context.ProductAttributeValue.Remove(productAttributeValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAttributeValueExists(int id)
        {
            return _context.ProductAttributeValue.Any(e => e.Id == id);
        }
    }
}
