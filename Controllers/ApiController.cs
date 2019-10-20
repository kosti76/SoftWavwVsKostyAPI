using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftWavwVsKostyAPI.Models;

namespace SoftWavwVsKostyAPI
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Year>>> GetYear()
        {
            return await _context.Year.ToListAsync();
        }
        [HttpGet]
        public dynamic GetData()

        {


            var result = (from y in _context.Year
                          select new
                          {
                              year = y.year,
                              circle1 = y.circle1,
                              circle2 = y.circle2,
                              categories = _context.Category
                                           .Where(cat => cat.year == y.year)
                                           .Select(cat => new
                                           {
                                               cat.title,
                                               cat.value,
                                               cat.maxvalue
                                           }).ToList(),
                          }).ToList();


            //string prettyResponse = JsonConvert.SerializeObject(result);


            return result;
        }
        // GET: api/Api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Year>> GetYear(short id)
        {
            var year = await _context.Year.FindAsync(id);

            if (year == null)
            {
                return NotFound();
            }

            return year;
        }

        // PUT: api/Api/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYear(short id, Year year)
        {
            if (id != year.code)
            {
                return BadRequest();
            }

            _context.Entry(year).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearExists(id))
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

        // POST: api/Api
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Year>> PostYear(Year year)
        {
            _context.Year.Add(year);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYear", new { id = year.code }, year);
        }

        // DELETE: api/Api/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Year>> DeleteYear(short id)
        {
            var year = await _context.Year.FindAsync(id);
            if (year == null)
            {
                return NotFound();
            }

            _context.Year.Remove(year);
            await _context.SaveChangesAsync();

            return year;
        }

        private bool YearExists(short id)
        {
            return _context.Year.Any(e => e.code == id);
        }
    }
}
