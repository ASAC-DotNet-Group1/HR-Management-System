using HR_Management_System.Data;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HR_Management_System.Models.Services
{
    public class SalarySlipService : ISalarySlip
    {
        private readonly HR_DbContext _context;

        public SalarySlipService(HR_DbContext context)
        {
            _context = context;
        }
        public async Task<double> CalculateSalary(int id,DateTime dateTime)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            List<Attendance> attendances = await _context.Attendances.Where(x => x.EmployeeID == id && x.StartShift == false && x.StartDate.Month == dateTime.Month).ToListAsync();
            List<Ticket> tickets = await _context.Tickets.Where(x => x.Emp_id == id && x.Status == Status.Approved && x.Date.Month == dateTime.Month).ToListAsync();

            double totalTickets = 0;
            foreach (Ticket ticket in tickets) totalTickets += ticket.Total;
            return attendances.Count + totalTickets;
        }

        public async Task AddSalarySlip(int id )
        {
            DateTime dateTime = DateTime.Now;
            

            SalarySlip salarySlip = new SalarySlip()
            {
                EmployeeID = id,
                Date = dateTime,
                Total = await CalculateSalary(id,dateTime)
            };
            _context.Entry(salarySlip).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalarySlip(int id)
        {
            SalarySlip SalarySlips = await _context.SalarySlips.FindAsync(id);

            _context.Entry(SalarySlips).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<SalarySlipDTO> GetSalarySlip(int id , int month)
        {
            var attendances = await _context.Attendances.Where(x => x.EmployeeID == id && x.StartDate.Month == month).ToListAsync();
            var tickets = await _context.Tickets.Where(x => x.Emp_id == id && x.Date.Month == month).ToListAsync();
            return await _context.SalarySlips.Select(x => new SalarySlipDTO()
            {
                Date = x.Date,
                EmployeeID = x.EmployeeID,
                Employee = new EmployeeDTO()
                {
                    ID = id,
                    Level = x.Employee.Level,
                    Name = x.Employee.Name,
                    DepartmentName = x.Employee.Department.Name
                },
                Attendances = attendances.Select(x => new AttendanceDTO()
                {
                    EndShift = x.EndDate,
                    EmployeeID = x.EmployeeID,
                    StartShift = x.StartDate,
                    Name = x.EmpName,
                }).ToList(),
                Ticket = tickets.Select(x => new TicketDTO()
                {
                    ID = x.ID,
                    Status = x.Status,
                    Comment = x.Comment,
                    Date = x.Date,
                    Type = x.Type.ToString(),
                }).ToList(),
                Total = x.Total,
            }).FirstOrDefaultAsync(x => x.EmployeeID == id && x.Date.Month == month);
        }

        public async Task<List<SalarySlipDTO>> GetSalarySlips()
        {
            List<Attendance> attendances = await _context.Attendances.ToListAsync();
            List<Ticket> tickets = await _context.Tickets.ToListAsync();
            return await _context.SalarySlips.Select(slip => new SalarySlipDTO()
            {
                Date = slip.Date,
                EmployeeID = slip.EmployeeID,
                Employee = new EmployeeDTO()
                {
                    ID = slip.Employee.ID,
                    Level = slip.Employee.Level,
                    Name = slip.Employee.Name,
                    DepartmentName = slip.Employee.Department.Name
                },
                Total = slip.Total,
            }).ToListAsync();
        }
        public async Task UpdateSalarySlip(int id, SalarySlip salarySlip)
        {
            _context.Entry(salarySlip).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task<SalarySlip> Find(int id)
        {
            return await _context.SalarySlips.FindAsync(id);
        }
    }
}