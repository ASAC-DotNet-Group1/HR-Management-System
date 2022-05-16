﻿using System;
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
        [HttpGet("salarySlip/{id}")]
        public async Task<SalarySlipDTO> GetSalarySlip(int id)
        {
            return await _employee.GetSalarySlip(id);
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



        //Date stuff
        #region DATE (ATTENDANCE, SALARY SLIPS, TICKETS)

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
                return await _employee.GetAllAttendancesInADate(year, month);
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
                return await _employee.GetAllAttendancesInADateForEmployee(id, year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        /// <summary>
        /// Return Salary Slips of all employees in a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("Salary-Slips/year/{year}/month/{month}")]
        public async Task<ActionResult<List<SalarySlipDTO>>> GetAllSalarySlipsInADate(int year, int month)
        {
            try
            {
                return await _employee.GetAllSalarySlipsInADate(year, month);
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
                return await _employee.GetAllSalarySlipsInADateForEmployee(id, year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Return Salary Slips of all employees in a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("Tickets/year/{year}/month/{month}")]
        public async Task<ActionResult<List<TicketDTO>>> GetAllTicketsInADate(int year, int month)
        {
            try
            {
                return await _employee.GetAllTicketsInADate(year, month);
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
        [HttpGet("Tickets/employee/{id}/year/{year}/month/{month}")]
        public async Task<ActionResult<List<TicketDTO>>> GetAllTicketsInADateForEmployee(int id, int year, int month)
        {
            try
            {
                return await _employee.GetAllTicketsInADateForEmployee(id, year, month);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






        #endregion






    }
}
