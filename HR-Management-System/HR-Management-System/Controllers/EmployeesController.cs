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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeesController(IEmployee employee)
        {
            _employee = employee;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployees()
        {
            return await _employee.GetEmployees();
        }


        // GET: api/Employees/Department/1   ********NEW*********
        [HttpGet("Department/{id}")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeesInSpecificDepartment(int id)
        {
            return await _employee.GetEmployeesInSpecificDepartment(id);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _employee.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }

            try
            {
                await _employee.UpdateEmployee(id, employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _employee.GetEmployee(id) == null)
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(AddEmployeeDTO employee)
        {
            return await _employee.AddEmployee(employee);

        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employee.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employee.DeleteEmployee(id);

            return NoContent();
        }

        [HttpPut("ID/{empID}/Department/{depID}")]
        public async Task<IActionResult> SetEmployeeToDepartment(int empID, int depID) // ********Updated*********
        {
            try
            {
                await _employee.SetEmployeeToDepartment(empID, depID);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
    }
}
