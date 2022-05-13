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
    public class SalarySlipsController : ControllerBase
    {
        private readonly ISalarySlip _salarySlip;

        public SalarySlipsController(ISalarySlip salarySlip)
        {
            _salarySlip = salarySlip;
        }

        // GET: api/SalarySlips
        [HttpGet]
        public async Task<ActionResult<List<SalarySlipDTO>>> GetSalarySlips()
        {
            return await _salarySlip.GetSalarySlips();
        }

        // GET: api/SalarySlips/5
        [HttpGet("{id}/Month/{month}")]
        public async Task<ActionResult<SalarySlipDTO>> GetSalarySlip(int id, int month)
        {
            var salarySlip = await _salarySlip.GetSalarySlip(id, month);

            if (salarySlip == null)
            {
                return NotFound();
            }

            return salarySlip;
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
                await _salarySlip.UpdateSalarySlip(id,salarySlip);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _salarySlip.Find(id)==null)
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
        [HttpPost ("{id}")]
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

    }
}
