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
        public double CalculateSalary(double baseSalary,List<Attendance> attendances, List<Ticket> tickets)
        {
            double totalAttendance = 0;
            double totalTickets = 0;
            foreach (var attendance in attendances) if (!attendance.StartShift) totalAttendance--;
            foreach (var ticket in tickets)
            {
                if (ticket.Type == Type.Leave) totalTickets -= baseSalary / 160;
                else if (ticket.Type == Type.Overtime) totalTickets += baseSalary / 160 * 1.5;
            };
            return (totalAttendance * (baseSalary / 320)) + totalTickets;
        }

        public async Task AddSalarySlip(int id )
        {
            DateTime dateTime = DateTime.Now;
            Employee employee = await _context.Employees.FindAsync( id );
            List<Attendance> attendances =  await _context.Attendances.Where(x => x.EmployeeID == id && x.StartShift == true && x.StartDate.Month == dateTime.Month).ToListAsync();
            List<Ticket> tickets = await _context.Tickets.Where(x => x.emp_id == id && x.Status == Status.Approved && x.Date.Month == dateTime.Month).ToListAsync();

            SalarySlip salarySlip = new SalarySlip()
            {
                EmployeeID = id,
                Date = dateTime,
                Total = CalculateSalary(employee.Salary, attendances, tickets)
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
                    Type = x.Type,
                    EmployeeName = x.EmpName,
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
                    StartShift = x.StartDate,
                    EmployeeID = x.EmployeeID,
                    EndShift = x.EndDate,
                    Name = x.EmpName
                }).ToList(),
                Ticket = tickets.Select(x => new TicketDTO()
                {
                    EmployeeName = x.EmpName,
                    Status = x.Status,
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