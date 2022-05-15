using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Data;
using HR_Management_System.Models;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly HR_DbContext _context;

        public PerformancesController(HR_DbContext context)
        {
            _context = context;
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformances()
        {
            return await _context.Performances.ToListAsync();
        }

        // GET: api/Performances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetPerformance(int id)
        {
            var performance = await _context.Performances.FindAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            return performance;
        }

        // PUT: api/Performances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformance(int id, Performance performance)
        {
            if (id != performance.ID)
            {
                return BadRequest();
            }

            _context.Entry(performance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(id))
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

        // POST: api/Performances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Performance>> PostPerformance(Performance performance)
        {
            _context.Performances.Add(performance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformance", new { id = performance.ID }, performance);
        }

        // DELETE: api/Performances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            var performance = await _context.Performances.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }

            _context.Performances.Remove(performance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performances.Any(e => e.ID == id);
        }
    }
}
