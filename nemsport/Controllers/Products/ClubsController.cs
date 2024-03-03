using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nemsport.Data;
using nemsport.DTOs.ProductDTOs;
using nemsport.Models.ProductModels;
using nemsport.Models.UserModels;

namespace nemsport.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly nemsportContext _context;

        public ClubsController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/Clubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetClub()
        {
            try
            {
                var clubs = await _context.Club.Select(club => new {
                    club.Id,
                    club.Name,
                    club.Initials,
                    club.Logo,
                    club.JoinDate,
                    club.Code,
                    ClubResponsible = club.ClubUsers
                        .Where(cu => cu.IsClubResponsible)
                        .Select(cu => new { cu.User.FirstName, cu.User.LastName, cu.User.Email })
                        .FirstOrDefault()
                }).ToListAsync();

                if (clubs == null || !clubs.Any())
                {
                    return NotFound("No clubs found.");
                }

                return clubs;
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(int id)
        {
            var club = await _context.Club.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            return club;
        }

        // PUT: api/Clubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(int id, Club club)
        {
            if (id != club.Id)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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

        // POST: api/Clubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Club>> PostClub([FromForm] ClubCreateDTO clubDTO, [FromForm] IFormFile clubLogo)
        {
            var filePath = await SaveImage(clubLogo, clubDTO.ClubName);

            var club = new Club
            {
                Name = clubDTO.ClubName,
                Initials = clubDTO.ClubInitials,
                Logo = filePath,
                JoinDate = clubDTO.JoinDate,
                Code = clubDTO.ClubCode
            };


            _context.Club.Add(club);
            await _context.SaveChangesAsync();

            if (clubDTO.ResponsibleUserId.HasValue)
            {
                var userExists = await _context.User.AnyAsync(u => u.Id == clubDTO.ResponsibleUserId.Value);
                if (!userExists)
                {
                    return BadRequest("Den valgte bruger eksiterer ikke");
                }
            }

            var clubUser = new ClubUser
            {
                ClubId = club.Id,
                UserId = clubDTO.ResponsibleUserId.Value,
                IsClubResponsible = true
            };
            _context.ClubUser.Add(clubUser);
            await _context.SaveChangesAsync(); // Ensure this line is present to save the clubUser

            return CreatedAtAction("GetClub", new { id = club.Id }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var club = await _context.Club.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Club.Remove(club);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubExists(int id)
        {
            return _context.Club.Any(e => e.Id == id);
        }

        private async Task<string?> SaveImage(IFormFile file, string clubName)
        {
            if (file == null || file.Length == 0) {
                return null;
            }

            var safeClubName = string.Join("_", clubName.Split(Path.GetInvalidFileNameChars()));

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedImages", "Clubs", "Logo", safeClubName);

            if(!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = $"{safeClubName}-logo{ Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/UploadedImages/Clubs/Logo/{safeClubName}/{fileName}";
        }
    }
}
