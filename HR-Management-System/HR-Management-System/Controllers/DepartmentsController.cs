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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin , User")]
        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            return await _department.GetDepartments();
        }

        [Authorize(Roles = "Admin , User")]
        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _department.GetDepartment(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            await _department.AddDepartment(department);

            return CreatedAtAction("GetDepartment", new { id = department.ID }, department);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _department.GetDepartment(id);

            if (department == null)
            {
                return NotFound();
            }

            await _department.DeleteDepartment(id);

            return NoContent();
        }
       
    }
}
