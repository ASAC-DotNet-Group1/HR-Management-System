using HR_Management_System.Models;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                var attendance = await _attendance.GetAttendance(id);
                return Ok(attendance);
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
                await _attendance.UpdateAttendance(id, attendance);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Content("Updated");
        }

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Arrival/{id}")]
        public async Task<ActionResult<Attendance>> Arrival(int id)
        {
            try
            {
                await _attendance.Arrival(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            try
            {
                await _attendance.DeleteAttendance(id);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
        [HttpGet("attendances/{id}")]
        public async Task<List<AttendanceDTO>> GetAllAttendance(int id)
        {
            return await _attendance.GetAllAttendance(id);
        }
        /// <summary>
        /// Return attendances of all employees in a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("attendances/year/{year}/month/{month}")]
        public async Task<ActionResult<List<AttendanceDTO>>> GetAllAttendancesInADate(int year, int month)
        {
            try
            {
                return await _attendance.GetAllAttendancesInADate(year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Return attendances of a specific employee during a specific month of a specific year
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("attendances/employee/{id}/year/{year}/month/{month}")]
        public async Task<ActionResult<List<AttendanceDTO>>> GetAllAttendancesInADateForEmployee(int id, int year, int month)
        {
            try
            {
                return await _attendance.GetAllAttendancesInADateForEmployee(id, year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}