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
                var performances = await _performance.GetAllPerformanceReports();
                if (performances == null)
                {
                    return NotFound("There are no performance reports available right now");
                }
                return performances;
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/Performances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<PerformanceDTO> AddPerformance(AddPerformanceDTO performance)
        {

            return await _performance.AddPerformance(performance);
        }

        // GET: api/Performances/5
        [HttpGet("Employee/{id}")]
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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET:
        //Get Reports for department
        [HttpGet("Department/{id}")]

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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET:
        //Get Reports for specific month
        [HttpGet("Year/{year}/Month/{month}")]
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
        [HttpGet("Employee/{id}/Year/{year}/Month/{month}")]
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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Performances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<PerformanceDTO> PutPerformance(int id, UpdatePerformanceDTO performance)
        {
            try
            {
                return await _performance.UpdatePerformance(id, performance);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
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