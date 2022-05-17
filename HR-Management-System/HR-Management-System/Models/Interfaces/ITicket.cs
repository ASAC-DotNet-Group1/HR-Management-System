using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface ITicket
    {
        Task<TicketDTO> CreateTicket(AddTicketDTO ticket);
        Task<List<TicketDTO>> GetTickets();
        Task<TicketDTO> GetTicket(int id);
        Task<TicketDTO> Accept(int id);
        Task<TicketDTO> Deny(int id);
        Task DeleteTicket(int id);
        Task<List<TicketDTO>> GetEmployeeTickets(int id);
        // Tickets Date part
        #region Tickets!!

        // X
        public Task<List<TicketDTO>> GetAllTicketsInADateForEmployee(int id, int year, int month);

        // X
        public Task<List<TicketDTO>> GetAllTicketsInADate(int year, int month);


        #endregion

    }
}