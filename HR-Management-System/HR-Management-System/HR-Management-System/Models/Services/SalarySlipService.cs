using HR_Management_System.Data;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Services
{
    public class SalarySlipService : ISalarySlip
    {
        private readonly HR_DbContext _context;

        public SalarySlipService(HR_DbContext context)
        {
            _context = context;
        }


        public async Task AddSalarySlip(SalarySlip salarySlip)
        {
            _context.Entry(salarySlip).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalarySlip(int id)
        {
            SalarySlip SalarySlips = await GetSalarySlip(id);

            _context.Entry(SalarySlips).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<SalarySlip> GetSalarySlip(int id)
        {
            return await _context.SalarySlips.FindAsync(id);
        }

        public async Task<List<SalarySlip>> GetSalarySlips()
        {
            return await _context.SalarySlips.ToListAsync();
        }

        public async Task UpdateSalarySlip(int id, SalarySlip salarySlip)
        {
            _context.Entry(salarySlip).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


    }
}

//SalarySlip salarySlip = new SalarySlip()
//{
//    Attendances = attendances,
//    Ticket = tickets,
//    Date = Date,
//    Employee = employee,
//    EmployeeID = employee.ID,
//    Total = department.BaseSalary + totalAttendance + totalTicket
//};
