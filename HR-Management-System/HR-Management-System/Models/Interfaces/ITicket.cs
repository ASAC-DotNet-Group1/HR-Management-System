using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface ITicket 
    {
        Task<Ticket> CreateTicket(Ticket ticket);
        Task<List<Ticket>> GetTickets();
        Task<Ticket> GetTicket(int id);
        Task<Ticket> UpdateTicket(int id, Ticket ticket);
        Task DeleteTicket(int id);
    }
}
