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

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendance _attendance;

        public AttendancesController(IAttendance attendance)
        {
            _attendance = attendance;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<List<Attendance>>> GetAttendances()
        {
            return await _attendance.GetAttendances();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _attendance.GetAttendance(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.ID)
            {
                return BadRequest();
            }
            try
            {
                await _attendance.UpdateAttendance(id,attendance);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _attendance.GetAttendance(id) == null)
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

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(AttendanceDTO attendance)
        {
            await _attendance.AddAttendance(attendance);
            return Ok(attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _attendance.GetAttendance(id);
            if (attendance == null)
            {
                return NotFound();
            }

            await _attendance.DeleteAttendance(id);

            return NoContent();
        }

    }
}
