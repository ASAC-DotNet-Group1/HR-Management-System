using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Data;
using HR_Management_System.Models;
using HR_Management_System.Models.Interfaces;
using HR_Management_System.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicket _ticket;

        public TicketsController(ITicket ticket)
        {
            _ticket = ticket;
        }

        [Authorize(Roles = "Admin , User")]
        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
            var tickets = await _ticket.GetTickets();
            if (tickets == null) return NoContent();
            return Ok(tickets);
        }

        [Authorize(Roles = "Admin , User")]
        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _ticket.GetTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.ID)
            {
                return BadRequest();
            }

            var modifiedTicket =  await _ticket.UpdateTicket(id,ticket);
            if (modifiedTicket == null) return NotFound();
            return Ok(modifiedTicket);
        }

        [Authorize(Roles = "Admin")]
        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            Ticket newTicket = await _ticket.CreateTicket(ticket);
            return Ok(newTicket);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                await _ticket.DeleteTicket(id);
                return NoContent();
            }

            catch (Exception) { return NotFound(); }
        }
        // GET: api/Tickets/Employee/8
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<List<TicketDTO>>> GetEmployeeTickets(int id)
        {
            try
            {
                List <TicketDTO> employeetickets = await _ticket.GetEmployeeTickets(id);
                return Ok(employeetickets);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
