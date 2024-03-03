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
    public class ClubUsersController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ClubUsersController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/ClubUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubUser>>> GetClubUser()
        {
            return await _context.ClubUser.ToListAsync();
        }

        // GET: api/ClubUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubUser>> GetClubUser(int id)
        {
            var clubUser = await _context.ClubUser.FindAsync(id);

            if (clubUser == null)
            {
                return NotFound();
            }

            return clubUser;
        }

        // PUT: api/ClubUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClubUser(int id, ClubUser clubUser)
        {
            if (id != clubUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(clubUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubUserExists(id))
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

        // POST: api/ClubUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClubUser>> PostClubUser(ClubUser clubUser)
        {
            _context.ClubUser.Add(clubUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClubUser", new { id = clubUser.Id }, clubUser);
        }

        // DELETE: api/ClubUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubUser(int id)
        {
            var clubUser = await _context.ClubUser.FindAsync(id);
            if (clubUser == null)
            {
                return NotFound();
            }

            _context.ClubUser.Remove(clubUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubUserExists(int id)
        {
            return _context.ClubUser.Any(e => e.Id == id);
        }
    }
}
