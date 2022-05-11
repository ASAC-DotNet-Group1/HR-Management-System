using HR_Management_System.Data;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly HR_DbContext _context;

        public EmployeeService(HR_DbContext context)
        {
            _context = context;
        }


        public async Task AddEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            Employee employee= await GetEmployee(id);

            _context.Entry(employee).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task UpdateEmployee(int id, Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
