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

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceDTO>> GetPerformance(int id)
        {
            try
            {
                return await _performance.GetPerformanceReport(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<List<PerformanceDTO>>> GetPerformances()
        {
            try
            {
                var performances =  await _performance.GetAllPerformanceReports();
                if(performances == null)
                {
                    return NotFound("There are no performance reports available right now");
                }
                return performances;
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/Performances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Performance>> AddPerformance(Performance performance)
        {
            // Note.
            await _performance.AddPerformance(performance);
            return CreatedAtAction("GetPerformance", new { id = performance.ID }, performance);
        }

        // GET: api/Performances/5
        [HttpGet("Performance/employee/{id}")]
        public async Task<ActionResult<List<PerformanceDTO>>> GetEmployeeReports(int id)
        {
            try
            {
                var performances = await _performance.EmployeePerformanceReports(id);
                if (performances == null)
                {
                    return NotFound($"No report was found for employee with ID: {id}");
                }
                return performances;
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        //GET:
        //Get Reports for department
        [HttpGet("Performance/Department/{id}")]

        public async Task<ActionResult<List<PerformanceDTO>>> DepartmentReports(int id)
        {
            try
            {
                var performances = await _performance.PerformanceReportsForDepartment(id);
                if (performances == null)
                {
                    return NotFound();
                }
                return performances;
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET:
        //Get Reports for specific month
        [HttpGet("Performance/year/{year}/month/{month}")]
        public async Task<ActionResult<List<PerformanceDTO>>> ReportsInMonth(int year, int month)
        {
            try
            {
                var performances = await _performance.PerformanceReportsInSpecificMonth(year, month);
                if (performances == null)
                {
                    return NotFound();
                }
                return performances;
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET:
        //Get Reports for employee in specific month
        [HttpGet("Performance/employee/{id}/year/{year}/month/{month}")]
        public async Task<ActionResult<List<PerformanceDTO>>> ReportsForEmployeeInMonth(int id, int year, int month)
        {
            try
            {
                var performances = await _performance.PerformanceReportsForEmployeeInSpecificMonth(id, year, month);
                if (performances == null)
                {
                    return NotFound();
                }
                return performances;
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
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
            try
            {
                await _performance.DeletePerformance(id);
                return Content("Deleted");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}