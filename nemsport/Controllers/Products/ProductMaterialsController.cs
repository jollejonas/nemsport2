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
    public class ProductMaterialsController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ProductMaterialsController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ProductMaterials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMaterial>>> GetProductMaterial()
        {
            return await _context.ProductMaterial.ToListAsync();
        }

        // GET: api/ProductMaterials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMaterial>> GetProductMaterial(int id)
        {
            var productMaterial = await _context.ProductMaterial.FindAsync(id);

            if (productMaterial == null)
            {
                return NotFound();
            }

            return productMaterial;
        }

        // PUT: api/ProductMaterials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMaterial(int id, ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return BadRequest();
            }

            _context.Entry(productMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMaterialExists(id))
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

        // POST: api/ProductMaterials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductMaterial>> PostProductMaterial(ProductMaterial productMaterial)
        {
            _context.ProductMaterial.Add(productMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductMaterial", new { id = productMaterial.Id }, productMaterial);
        }

        // DELETE: api/ProductMaterials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMaterial(int id)
        {
            var productMaterial = await _context.ProductMaterial.FindAsync(id);
            if (productMaterial == null)
            {
                return NotFound();
            }

            _context.ProductMaterial.Remove(productMaterial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductMaterialExists(int id)
        {
            return _context.ProductMaterial.Any(e => e.Id == id);
        }
    }
}
