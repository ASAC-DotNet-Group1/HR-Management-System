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


        public async Task AddSalarySlip(int id )

        {
            Employee employee = await _context.Employees.FindAsync( id );
            var attendances =  await _context.Attendances.Where(x => x.EmployeeID == id).ToListAsync();
            var tickets = await _context.Tickets.Where(x => x.emp_id == id).ToListAsync();
            DateTime dateTime = DateTime.Now;
            int totalAttendance = 0;
            int totalTickets = 0;
            foreach( var attendance in attendances) if (!attendance.Present) totalAttendance++;
            foreach (var ticket in tickets) totalTickets += (int)ticket.Type;
            SalarySlip salarySlip = new SalarySlip()
            { 
                EmployeeID = id,
                Date = dateTime,
                Total = employee.Salary + totalTickets - totalAttendance
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
            var attendances = await _context.Attendances.Where(x => x.EmployeeID == id).ToListAsync();
            var tickets = await _context.Tickets.Where(x => x.emp_id == id).ToListAsync();
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
                    Date = x.Date,
                    EmployeeID = x.EmployeeID,
                    Present = x.Present,
                }).ToList(),
                Ticket = tickets.Select(x => new TicketDTO()
                {
                    ID = x.ID,
                    Approval = x.Approval,
                    Comment = x.Comment,
                    Date = x.Date,
                    Type = x.Type,
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
                Attendances = attendances.Select(x => new AttendanceDTO()
                {
                    Date = x.Date,
                    EmployeeID = x.EmployeeID,
                    Present = x.Present,
                }).ToList(),
                Ticket = tickets.Select(x => new TicketDTO()
                {
                    Approval = x.Approval,
                    Date = x.Date,
                    Type = x.Type,
                    Comment = x.Comment,
                    ID = x.ID,
                }).ToList(),
                Total = slip.Total,
            }).ToListAsync();
        }
        //.Where(x => x.EmployeeID == slip.EmployeeID)

        //.Where(x => x.emp_id == slip.EmployeeID)
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