using HR_Management_System.Data;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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


        public async Task AddDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Added;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteDepartment(int id)
        {
            Department department = await GetDepartment(id);

            _context.Entry(department).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task UpdateDepartment(int id, Department department)
        {
            _context.Entry(department).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
