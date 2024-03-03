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
    public class ParentCategoriesController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ParentCategoriesController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ParentCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentCategory>>> GetParentCategory()
        {
            return await _context.ParentCategory.ToListAsync();
        }

        // GET: api/ParentCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentCategory>> GetParentCategory(int id)
        {
            var parentCategory = await _context.ParentCategory.FindAsync(id);

            if (parentCategory == null)
            {
                return NotFound();
            }

            return parentCategory;
        }

        // PUT: api/ParentCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParentCategory(int id, ParentCategory parentCategory)
        {
            if (id != parentCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(parentCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentCategoryExists(id))
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

        // POST: api/ParentCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParentCategory>> PostParentCategory(ParentCategory parentCategory)
        {
            _context.ParentCategory.Add(parentCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParentCategory", new { id = parentCategory.Id }, parentCategory);
        }

        // DELETE: api/ParentCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParentCategory(int id)
        {
            var parentCategory = await _context.ParentCategory.FindAsync(id);
            if (parentCategory == null)
            {
                return NotFound();
            }

            _context.ParentCategory.Remove(parentCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParentCategoryExists(int id)
        {
            return _context.ParentCategory.Any(e => e.Id == id);
        }
    }
}
