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
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _employee.AddEmployee(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.ID }, employee);
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
        [HttpGet("salarySlip/{id}")]
        public async Task<SalarySlipDTO> GetSalarySlip(int id)
        {
            return await _employee.GetSalarySlip(id);
        }
        [HttpGet("dep/{id}")]
        public async Task<DepartmentDTO> GetDepartmentForEmployee(int id)
        {
            return await _employee.GetDepartmentForEmployee(id);
        }
        [HttpPut("{empId}/{depId}")]
        public async Task<IActionResult> SetEmployeeToDepartment(int empId, int depId)
        {
            await _employee.SetEmployeeToDepartment(empId, depId);
            
            return Ok();
        }


      


        [HttpGet("attendances/{id}")]
        public async Task<List<AttendanceDTO>> GetAllAttendance(int id)
        {
            return await _employee.GetAllAttendance(id);
        }


        [HttpGet("shiftends/{id}")]
        public async Task<List<ShiftEndDTO>> GetAllShiftEnds(int id)
        {
            return await _employee.GetAllShiftEnds(id);
        }


    }
}
