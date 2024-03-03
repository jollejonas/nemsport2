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
    public class ClubProductsController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ClubProductsController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ClubProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubProduct>>> GetClubProduct()
        {
            return await _context.ClubProduct.ToListAsync();
        }

        // GET: api/ClubProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubProduct>> GetClubProduct(int id)
        {
            var clubProduct = await _context.ClubProduct.FindAsync(id);

            if (clubProduct == null)
            {
                return NotFound();
            }

            return clubProduct;
        }

        // PUT: api/ClubProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClubProduct(int id, ClubProduct clubProduct)
        {
            if (id != clubProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(clubProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubProductExists(id))
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

        // POST: api/ClubProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClubProduct>> PostClubProduct(ClubProduct clubProduct)
        {
            _context.ClubProduct.Add(clubProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClubProduct", new { id = clubProduct.Id }, clubProduct);
        }

        // DELETE: api/ClubProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubProduct(int id)
        {
            var clubProduct = await _context.ClubProduct.FindAsync(id);
            if (clubProduct == null)
            {
                return NotFound();
            }

            _context.ClubProduct.Remove(clubProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubProductExists(int id)
        {
            return _context.ClubProduct.Any(e => e.Id == id);
        }
    }
}
