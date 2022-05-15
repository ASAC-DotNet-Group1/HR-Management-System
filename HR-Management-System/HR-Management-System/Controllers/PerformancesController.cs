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
    public class PerformancesController : ControllerBase
    {
        private readonly IPerformance _performance;

        public PerformancesController(IPerformance performance)
        {
            _performance = performance;
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<List<PerformanceDTO>>> GetPerformances()
        {
            return await _performance.GetAllPerformanceReports();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceDTO>> GetPerformance(int id)
        {
            var performance = await _performance.GetPerformanceReport(id);

            if (performance == null)
            {
                return NotFound();
            }

            return performance;
        }

        // POST: api/Performances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Performance>> AddPerformance(Performance performance)
        {
            await _performance.AddPerformance(performance);
            return CreatedAtAction("GetPerformance", new { id = performance.ID }, performance);
        }
        // GET: api/Performances/5
        [HttpGet("Performance/employee/{id}")]
        public async Task<ActionResult<List<PerformanceDTO>>> GetEmployeeReports(int id)
        {
            var performances = await _performance.EmployeePerformanceReports(id);

            if (performances == null)
            {
                return NotFound();
            }

            return performances;
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

            try
            {
                await _performance.UpdatePerformance(id, performance);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _performance.GetPerformanceReport(id) == null)
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


        // DELETE: api/Performances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            var employee = await _performance.GetPerformanceReport(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _performance.DeletePerformance(id);

            return NoContent(); 
        }
        //GET:
        //Get Reports for department
        [HttpDelete("Performance/Department/{id}")]
        public async Task<ActionResult<List<PerformanceDTO>>> DepartmentReports(string name)
        {
            var performances = await _performance.PerformanceReportsForDepartment(name);
            if (performances == null)
            {
                return NotFound();
            }

            return performances;
        }

        //GET:
        //Get Reports for specific month
        [HttpDelete("Performance/year/{year}/month/{month}")]
        public async Task<ActionResult<List<PerformanceDTO>>> ReportsInMonth(int year, int month)
        {
            var performances = await _performance.PerformanceReportsInSpecificMonth( year,  month);
            if (performances == null)
            {
                return NotFound();
            }

            return performances;
        }
        //GET:
        //Get Reports for employee in specific month
        [HttpDelete("Performance/employee/{id}/year/{year}/month/{month}")]
        public async Task<ActionResult<List<PerformanceDTO>>> ReportsForEmployeeInMonth(int id, int year, int month)
        {
            var performances = await _performance.PerformanceReportsForEmployeeInSpecificMonth(id,year, month);
            if (performances == null)
            {
                return NotFound();
            }

            return performances;
        }
    }
}