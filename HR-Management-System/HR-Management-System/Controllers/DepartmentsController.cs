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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartment _department;

        public DepartmentsController(IDepartment department)
        {
            _department = department;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDTO>>> GetDepartments()
        {
            try
            {
                return await _department.GetDepartments();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            try
            {
                return await _department.GetDepartment(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.ID)
            {
                return BadRequest();
            }


            try
            {
                await _department.UpdateDepartment(id, department);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _department.GetDepartment(id) == null)
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            await _department.AddDepartment(department);

            return CreatedAtAction("GetDepartment", new { id = department.ID }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _department.GetDepartment(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok("Deleted");
        }
    }
}