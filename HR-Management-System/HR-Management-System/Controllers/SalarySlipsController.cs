using HR_Management_System.Models;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalarySlipsController : ControllerBase
    {
        private readonly ISalarySlip _salarySlip;

        public SalarySlipsController(ISalarySlip salarySlip)
        {
            _salarySlip = salarySlip;
        }

        // GET: api/SalarySlips
        [HttpGet]
        public async Task<ActionResult<List<ShortenedSalarySlipDTO>>> GetSalarySlips()
        {
            return await _salarySlip.GetSalarySlips();
        }

        // PUT: api/SalarySlips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalarySlip(int id, SalarySlip salarySlip)
        {
            if (id != salarySlip.EmployeeID)
            {
                return BadRequest();
            }
            try
            {
                await _salarySlip.UpdateSalarySlip(id, salarySlip);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _salarySlip.Find(id) == null)
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

        // POST: api/SalarySlips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<SalarySlip>> PostSalarySlip(int id)
        {
            try
            {
                await _salarySlip.AddSalarySlip(id);
            }
            catch (DbUpdateException)
            {

                throw;

            }
            return Ok();
        }

        // DELETE: api/SalarySlips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalarySlip(int id)
        {
            var salarySlip = await _salarySlip.Find(id);
            if (salarySlip == null)
            {
                return NotFound();
            }

            await _salarySlip.DeleteSalarySlip(id);
            return NoContent();
        }

        /// <summary>
        /// Return Salary Slips of all employees in a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("Salary-Slips/year/{year}/month/{month}")]
        public async Task<ActionResult<List<ShortenedSalarySlipDTO>>> GetAllSalarySlipsInADate(int year, int month)
        {
            try
            {
                return await _salarySlip.GetAllSalarySlipsInADate(year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Return salary slips of a specific employee during a specific month of a specific year
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Salary-Slips/employee/{id}/year/{year}/month/{month}")]
        public async Task<ActionResult<List<SalarySlipDTO>>> GetAllSalarySlipsInADateForEmployee(int id, int year, int month)
        {
            try
            {
                return await _salarySlip.GetAllSalarySlipsInADateForEmployee(id, year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
