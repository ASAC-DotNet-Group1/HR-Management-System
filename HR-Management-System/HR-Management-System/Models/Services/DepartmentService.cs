using HR_Management_System.Data;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly HR_DbContext _context;

        public DepartmentService(HR_DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new deparment in the database
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public async Task AddDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a deparment from the database using the department ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteDepartment(int id)
        {
            Department department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                throw new Exception("Department was not found");
            }
            _context.Entry(department).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        // Get a department from the database using the department's ID
        public async Task<DepartmentDTO> GetDepartment(int id)
        {
            Department department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                throw new Exception("Department was not found");
            }
            return new DepartmentDTO
            {
                Name = department.Name,
                Employees = await _context.Employees.Where(x => x.DepartmentID == id).Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    Age = x.Age,
                    DepartmentName = x.Department.Name,
                    DepartmentID = x.DepartmentID,
                    Email = x.Email,
                    Gender = x.Gender,
                    Level = x.Level.ToString(),
                    LeaveCredit = x.LeaveCredit,
                    Phone = x.Phone,
                    VacationCredit = x.VacationCredit

                }).ToListAsync()
            };
        }


        /// <summary>
        /// Get all departments in my database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<DepartmentDTO>> GetDepartments()
        {
            var departments = await _context.Departments.Select(x => new DepartmentDTO
            {
                Name = x.Name,
                Employees = x.Employees.Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    Age = x.Age,
                    DepartmentName = x.Department.Name,
                    DepartmentID = x.DepartmentID,
                    Email = x.Email,
                    Gender = x.Gender,
                    Level = x.Level.ToString(),
                    LeaveCredit = x.LeaveCredit,
                    Phone = x.Phone,
                    VacationCredit = x.VacationCredit
                }).ToList()
            }).ToListAsync();

            if (departments == null)
            {
                throw new Exception("No departments found");
            }

            return departments;
        }


        /// <summary>
        /// Update a department in the databse by using the department ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public async Task UpdateDepartment(int id, Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
