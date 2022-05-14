using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Data;
using HR_Management_System.Models;
using HR_Management_System.Models.Interfaces;
using HR_Management_System.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftEndsController : ControllerBase
    {
        private readonly IShiftEnd _ShiftEnd;

        public ShiftEndsController(IShiftEnd ShiftEnd)
        {
            _ShiftEnd = ShiftEnd;
        }

        [Authorize(Roles = "Admin , User")]
        // GET: api/ShiftEnds
        [HttpGet]
        public async Task<ActionResult<List<ShiftEndDTO>>> GetShiftEnds()
        {
            return await _ShiftEnd.GetShiftEnds();
        }

        [Authorize(Roles = "Admin , User")]
        // GET: api/ShiftEnds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftEnd>> GetShiftEnd(int id)
        {
            var ShiftEnd = await _ShiftEnd.GetShiftEnd(id);

            if (ShiftEnd == null)
            {
                return NotFound();
            }

            return ShiftEnd;
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/ShiftEnds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftEnd(int id, ShiftEnd ShiftEnd)
        {
            if (id != ShiftEnd.ID)
            {
                return BadRequest();
            }
            try
            {
                await _ShiftEnd.UpdateShiftEnd(id, ShiftEnd);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _ShiftEnd.GetShiftEnd(id) == null)
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

        [Authorize(Roles = "Admin")]
        // POST: api/ShiftEnds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftEnd>> PostShiftEnd(ShiftEndDTO ShiftEnd)
        {
            await _ShiftEnd.AddShiftEnd(ShiftEnd);
            return Ok(ShiftEnd);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/ShiftEnds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftEnd(int id)
        {
            var ShiftEnd = await _ShiftEnd.GetShiftEnd(id);
            if (ShiftEnd == null)
            {
                return NotFound();
            }

            await _ShiftEnd.DeleteShiftEnd(id);

            return NoContent();
        }

        

    }
}
