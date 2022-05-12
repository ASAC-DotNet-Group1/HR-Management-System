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
        public async Task<ActionResult<List<AttendanceDTO>>> GetAttendances()
        {
            return await _attendance.GetAttendances();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDTO>> GetAttendance(int id)
        {
            try
            {
                return await _attendance.GetAttendance(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
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
        public async Task<ActionResult<AttendanceDTO>> PostAttendance(AttendanceDTO attendancedto)
        {
            try
            {
                await _attendance.AddAttendance(attendancedto);
            }
            catch (Exception e) 
            { 
                return BadRequest(e.Message);
            }
            return attendancedto;
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
