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
    public class EmployeeService : IEmployee
    {
        private readonly HR_DbContext _context;
        private readonly ISalarySlip _salarySlip;

        public EmployeeService(HR_DbContext context, ISalarySlip salarySlip)
        {
            _context = context;
            _salarySlip = salarySlip;
        }


        /// <summary>
        /// Create a new employee inside my Database
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        public async Task<EmployeeDTO> AddEmployee(AddEmployeeDTO newEmployee)
        {
            Employee employee = new Employee
            {
                Name = newEmployee.Name,
                DepartmentID = newEmployee.DepartmentID,
                Age = newEmployee.Age,
                Email = newEmployee.Email,
                Gender = newEmployee.Gender,
                Level = newEmployee.Level,
                Password = newEmployee.Password,
                Phone = newEmployee.Phone,
                Salary = newEmployee.Salary,
                LeaveCredit = 14,
                VacationCredit = 14,
            };
            _context.Entry(employee).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new EmployeeDTO
            {
                ID = employee.ID,
                Age = employee.Age,
                DepartmentID = employee.DepartmentID,
                Email = employee.Email,
                Gender = employee.Gender,
                LeaveCredit = employee.LeaveCredit,
                Level = employee.Level.ToString(),
                Name = employee.Name,
                Phone = employee.Phone,
                VacationCredit = employee.VacationCredit,
                DepartmentName = employee.Department.Name,
            };
        }

        /// <summary>
        /// Delete an employee from my database by passing the employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            _context.Entry(employee).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get an employee from the database using his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            return await _context.Employees
                .Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    Level = x.Level.ToString(),
                    Age = x.Age,
                    Email = x.Email,
                    Phone = x.Phone,
                    Gender = x.Gender,
                    LeaveCredit = x.LeaveCredit,
                    VacationCredit = x.VacationCredit,
                    DepartmentName = x.Department.Name
                }).FirstAsync();
        }

        /// <summary>
        /// Get all employees in my database
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeDTO>> GetEmployees()
        {

            return await _context.Employees
                .Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    Level = x.Level.ToString(),
                    Age = x.Age,
                    Email = x.Email,
                    Phone = x.Phone,
                    Gender = x.Gender,
                    LeaveCredit = x.LeaveCredit,
                    VacationCredit = x.VacationCredit,
                    DepartmentName = x.Department.Name
                }).ToListAsync();
        }

        /// <summary>
        /// Update a specific employee in my database using the employee's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task UpdateEmployee(int id, Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Set or Transfer an employee to a department
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task SetEmployeeToDepartment(int empId, int departmentId)
        {
            Employee employee = await _context.Employees.FindAsync(empId);
            employee.DepartmentID = departmentId;
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

}