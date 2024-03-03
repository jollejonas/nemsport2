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

namespace nemsport.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseProductsController : ControllerBase
    {
        private readonly nemsportContext _context;

        public BaseProductsController(nemsportContext context)
        {
            _context = context;
        }

        // GET: api/BaseProducts1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBaseProduct()
        {
                var baseProduct = await _context.BaseProduct
                    .Include(bp => bp.ProductSports)
                    .ThenInclude(ps => ps.Sport)
                    .Select(baseProduct => new {
                    baseProduct.Id,
                    baseProduct.Name,
                    baseProduct.BasePrice,
                    baseProduct.Description,
                    baseProduct.Gender,
                    baseProduct.CollectionId,
                    CollectionName = baseProduct.Collection.Name,
                    baseProduct.CreationDate,
                    baseProduct.ExpiryDate,
                    baseProduct.UnitType,
                    Sports = baseProduct.ProductSports.Select(ps => new
                    {
                        ps.SportId,
                        SportName = ps.Sport.Name
                    }).ToList()
                }).ToListAsync();

                return baseProduct;
        }

        // GET: api/BaseProducts1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseProduct>> GetBaseProduct(int id)
        {
            var baseProduct = await _context.BaseProduct.FindAsync(id);

            if (baseProduct == null)
            {
                return NotFound();
            }

            return baseProduct;
        }

        // PUT: api/BaseProducts1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseProduct(int id, BaseProduct baseProduct)
        {
            if (id != baseProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(baseProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseProductExists(id))
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

        // POST: api/BaseProducts1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaseProduct>> PostBaseProduct([FromBody] BaseProductCreateDTO baseProductDTO)
        {
            if (!_context.Collection.Any(c => c.Id == baseProductDTO.CollectionId))
            {
                return BadRequest("Collection not found.");
            }

            var baseProduct = new BaseProduct
            {
                Name = baseProductDTO.Name,
                BasePrice = baseProductDTO.BasePrice,
                Description = baseProductDTO.Description,
                Gender = baseProductDTO.Gender,
                CollectionId = baseProductDTO.CollectionId,
                CreationDate = baseProductDTO.CreationDate,
                ExpiryDate = baseProductDTO.ExpiryDate,
                UnitType = baseProductDTO.UnitType,
            };


            _context.BaseProduct.Add(baseProduct);
            await _context.SaveChangesAsync();

            if (baseProductDTO.SportIds != null && baseProductDTO.SportIds.Any())
            {
                foreach (var sportId in baseProductDTO.SportIds)
                {
                    var productSport = new ProductSport
                    {
                        BaseProductId = baseProduct.Id,
                        SportId = sportId
                    };
                    _context.ProductSport.Add(productSport);
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetBaseProduct", new { id = baseProduct.Id }, baseProduct);
        }

        // DELETE: api/BaseProducts1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaseProduct(int id)
        {
            var baseProduct = await _context.BaseProduct.FindAsync(id);
            if (baseProduct == null)
            {
                return NotFound();
            }

            _context.BaseProduct.Remove(baseProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BaseProductExists(int id)
        {
            return _context.BaseProduct.Any(e => e.Id == id);
        }
    }
}
