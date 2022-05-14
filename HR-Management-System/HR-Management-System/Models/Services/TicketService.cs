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
        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            ticket.Date = DateTime.Now.ToLocalTime();
            _context.Entry(ticket).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> GetTicket(int id)
        {
            
            Ticket ticket = await _context.Tickets.FindAsync(id);
            return ticket;
        }

        public async Task<List<Ticket>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            if (tickets == null) throw new Exception("There are no tickets available");
            return tickets;
        }

        public async Task<Ticket> UpdateTicket(int id, Ticket ticket)
        {
            Ticket oldTicket = await GetTicket(id);
            if (oldTicket == null) return null;
            oldTicket.Status = ticket.Status;
            _context.Entry(oldTicket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldTicket;
        }
        public async Task DeleteTicket(int id)
        {
            Ticket ticket = await GetTicket(id);
            if (ticket == null) { throw new Exception($" There is no Ticket with id : {id} ! "); }
            _context.Entry(ticket).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
        public async Task<List<TicketDTO>> GetEmployeeTickets(int id)
        {
            
            Employee employee =  _context.Employees.Find(id);
            if (employee == null) { throw new Exception(); }
            return await _context.Tickets.Where(x => x.emp_id == id).Select(x => new TicketDTO
            {
                ID = x.ID,
                EmployeeName = x.EmpName,
                Type = x.Type,
                Date = x.Date,
                Comment = x.Comment,
                Status = x.Status,
                Level = employee.Level.ToString()
                
            }).ToListAsync();
            
        }

    }
}
