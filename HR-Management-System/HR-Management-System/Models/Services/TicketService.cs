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
    public class TicketService : ITicket
    {
        private readonly HR_DbContext _context;

        public TicketService(HR_DbContext context)
        {
            _context = context;
        }
        public async Task<TicketDTO> CreateTicket(AddTicketDTO newticket)
        {
            DateTime date = DateTime.Now.ToLocalTime();
            Employee employee = await _context.Employees.FindAsync(newticket.Emp_id);
            Ticket ticket = new Ticket()
            {
                Amount = newticket.Amount,
                Comment = newticket.Comment,
                Date = date,
                EmpName = employee.Name,
                Status = Status.Pending,
                Type = newticket.Type,
                Emp_id = newticket.Emp_id,
            };
            _context.Entry(ticket).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new TicketDTO() 
            {
                Comment = ticket.Comment,
                Date = ticket.Date,
                EmployeeName = employee.Name,
                ID = ticket.ID,
                Status = ticket.Status,
                Type = ticket.Type.ToString(),
            };
        }
       
        public async Task CalculateTotal (Employee employee, Ticket ticket )
        {
            if (ticket.Type == Type.Leave)
            {
                int n = employee.LeaveCredit - ticket.Amount;
                if (n <= 0)
                    ticket.Total = (employee.Salary / 160) * n;
                else ticket.Total = 0;
            }
            else if (ticket.Type == Type.Vacation)
            {
                int n = employee.VacationCredit - ticket.Amount;
                if (n <= 0)
                    ticket.Total = (employee.Salary / 160) * n * 8;
                else ticket.Total = 0;
            }
            else if (ticket.Type == Type.Overtime)
            {
                ticket.Total = (employee.Salary / 160) * ticket.Amount * 1.5;
            }
            else throw new Exception("Ticket Type is not Valid");
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        public async Task<TicketDTO> Accept(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            Ticket ticket = await _context.Tickets.FindAsync(id);
            ticket.Status = Status.Approved;
            _context.Entry(ticket).State = EntityState.Modified;
            await CalculateTotal(employee, ticket);
            if (ticket.Type == Type.Leave) employee.LeaveCredit -= ticket.Amount;
            else if (ticket.Type == Type.Vacation) employee.VacationCredit -= ticket.Amount;
            if (employee.LeaveCredit <0) employee.LeaveCredit = 0;
            if(employee.VacationCredit <0) employee.VacationCredit = 0;
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetTicket(id);
        }
        public async Task<TicketDTO> Deny(int id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
            ticket.Status = Status.Denied;
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetTicket(id);

        }

        public async Task<TicketDTO> GetTicket(int id)
        {
            
            Ticket ticket = await _context.Tickets.FindAsync(id);
            return new TicketDTO
            {
                Comment = ticket.Comment,
                Date = ticket.Date,
                ID = ticket.ID,
                EmployeeName = ticket.EmpName,
                Status = ticket.Status,
                Type = ticket.Type.ToString()
            };
        }

        public async Task<List<TicketDTO>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            if (tickets == null) throw new Exception("There are no tickets available");
            return await _context.Tickets.Select(x => new TicketDTO 
            {
                Comment = x.Comment,
                Date = x.Date,
                ID = x.ID,
                EmployeeName = x.EmpName,
                Status = x.Status,
                Type = x.Type.ToString()
            }).ToListAsync();
        }

       
        public async Task DeleteTicket(int id)
        {
            Ticket ticket = await Find(id);
            if (ticket == null) { throw new Exception($" There is no Ticket with id : {id} ! "); }
            _context.Entry(ticket).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
        public async Task<List<TicketDTO>> GetEmployeeTickets(int id)
        {
            
            Employee employee =  _context.Employees.Find(id);
            if (employee == null) { throw new Exception(); }
            return await _context.Tickets.Where(x => x.Emp_id == id).Select(x => new TicketDTO
            {
                ID = x.ID,
                EmployeeName = employee.Name,
                Type = x.Type.ToString(),
                Date = x.Date,
                Comment = x.Comment,
                Status = x.Status,
                
            }).ToListAsync();
            
        }
        public async Task<Ticket> Find(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }
    }
}
