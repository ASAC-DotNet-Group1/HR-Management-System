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


        /// <summary>
        /// Get ticket from database using the ticked ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TicketDTO> GetTicket(int id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
            if(ticket == null) { return null; }
            return new TicketDTO
            {
                Comment = ticket.Comment,
                Date = ticket.Date,
                ID = ticket.ID,
                EmployeeName = ticket.Employee.Name,
                Status = ticket.Status.ToString(),
                Type = ticket.Type.ToString(),
                Employee = new EmployeeDTO
                {
                    ID = ticket.Employee.ID,
                    DepartmentID = ticket.Employee.DepartmentID,
                    Name = ticket.Employee.Name,
                    Level = ticket.Employee.Level.ToString(),
                    Age = ticket.Employee.Age,
                    Email = ticket.Employee.Email,
                    Phone = ticket.Employee.Phone,
                    Gender = ticket.Employee.Gender,
                    LeaveCredit = ticket.Employee.LeaveCredit,
                    VacationCredit = ticket.Employee.VacationCredit,
                    DepartmentName = ticket.Employee.Department.Name
                }
            };
        }

        public async Task<TicketDTO> CreateTicket(AddTicketDTO newticket)
        {
            DateTime date = DateTime.Now.ToLocalTime();
            Employee employee = await _context.Employees.FindAsync(newticket.EmployeeID);
            if (employee == null) throw new Exception("Employee was not found");
            Ticket ticket = new Ticket()
            {
                Amount = newticket.Amount,
                Comment = newticket.Comment,
                Date = date,
                Status = Status.Pending,
                Type = newticket.Type,
                EmployeeID = newticket.EmployeeID,
            };
            _context.Entry(ticket).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new TicketDTO()
            {
                Comment = ticket.Comment,
                Date = ticket.Date,
                EmployeeName = employee.Name,
                ID = ticket.ID,
                Status = ticket.Status.ToString(),
                Type = ticket.Type.ToString(),
                Employee = new EmployeeDTO
                {
                    ID = ticket.Employee.ID,
                    DepartmentID = ticket.Employee.DepartmentID,
                    Name = ticket.Employee.Name,
                    Level = ticket.Employee.Level.ToString(),
                    Age = ticket.Employee.Age,
                    Email = ticket.Employee.Email,
                    Phone = ticket.Employee.Phone,
                    Gender = ticket.Employee.Gender,
                    LeaveCredit = ticket.Employee.LeaveCredit,
                    VacationCredit = ticket.Employee.VacationCredit,
                    DepartmentName = ticket.Employee.Department.Name
                }
            };
        }


        /// <summary>
        /// Calculate total tickets & it's effect on salary????!?!??!?!?!?!!?!?!?!?
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CalculateTotal(Employee employee, Ticket ticket)
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


        /// <summary>
        ///  Accept a pending ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TicketDTO> Accept(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) { throw new Exception("Employee was not found"); }
            Ticket ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) { throw new Exception("Ticket was not found"); }
            ticket.Status = Status.Approved;
            _context.Entry(ticket).State = EntityState.Modified;
            await CalculateTotal(employee, ticket);
            if (ticket.Type == Type.Leave) employee.LeaveCredit -= ticket.Amount;
            else if (ticket.Type == Type.Vacation) employee.VacationCredit -= ticket.Amount;
            if (employee.LeaveCredit < 0) employee.LeaveCredit = 0;
            if (employee.VacationCredit < 0) employee.VacationCredit = 0;
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetTicket(id);
        }


        /// <summary>
        /// Deny a pending Ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TicketDTO> Deny(int id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
            if(ticket == null) { throw new Exception("Ticket was not found"); }
            ticket.Status = Status.Denied;
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetTicket(id);
        }


        /// <summary>
        /// Get all tickets from Database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<TicketDTO>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            if (tickets == null) throw new Exception("There are no tickets available");
            return await _context.Tickets.Select(x => new TicketDTO
            {
                Comment = x.Comment,
                Date = x.Date,
                ID = x.ID,
                EmployeeName = x.Employee.Name,
                Status = x.Status.ToString(),
                Type = x.Type.ToString(),
                Employee = new EmployeeDTO
                {
                    ID = x.Employee.ID,
                    DepartmentID = x.Employee.DepartmentID,
                    Name = x.Employee.Name,
                    Level = x.Employee.Level.ToString(),
                    Age = x.Employee.Age,
                    Email = x.Employee.Email,
                    Phone = x.Employee.Phone,
                    Gender = x.Employee.Gender,
                    LeaveCredit = x.Employee.LeaveCredit,
                    VacationCredit = x.Employee.VacationCredit,
                    DepartmentName = x.Employee.Department.Name
                }
            }).ToListAsync();
        }


        /// <summary>
        /// Delete a specific ticket from the database using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteTicket(int id)
        {
            Ticket ticket = await Find(id);
            if (ticket == null) { throw new Exception($" There is no Ticket with id : {id}."); }
            _context.Entry(ticket).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Get tickets of a specific employee using employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<TicketDTO>> GetEmployeeTickets(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            return await _context.Tickets.Where(x => x.EmployeeID == id).Select(x => new TicketDTO
            {
                ID = x.ID,
                EmployeeName = employee.Name,
                Type = x.Type.ToString(),
                Date = x.Date,
                Comment = x.Comment,
                Status = x.Status.ToString(),
                Employee = new EmployeeDTO
                {
                    ID = x.Employee.ID,
                    DepartmentID = x.Employee.DepartmentID,
                    Name = x.Employee.Name,
                    Level = x.Employee.Level.ToString(),
                    Age = x.Employee.Age,
                    Email = x.Employee.Email,
                    Phone = x.Employee.Phone,
                    Gender = x.Employee.Gender,
                    LeaveCredit = x.Employee.LeaveCredit,
                    VacationCredit = x.Employee.VacationCredit,
                    DepartmentName = x.Employee.Department.Name
                }

            }).ToListAsync();

        }

        /// <summary>
        /// Get all tickets of an employee using his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<TicketDTO>> GetAllTickets(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            return await _context.Tickets.Where(x => x.EmployeeID == id).Select(x => new TicketDTO()
            {
                Comment = x.Comment,
                Date = x.Date,
                EmployeeName = employee.Name,
                ID = x.ID,
                Status = x.Status.ToString(),
                Type = x.Type.ToString(),
                Employee = new EmployeeDTO
                {
                    ID = x.Employee.ID,
                    DepartmentID = x.Employee.DepartmentID,
                    Name = x.Employee.Name,
                    Level = x.Employee.Level.ToString(),
                    Age = x.Employee.Age,
                    Email = x.Employee.Email,
                    Phone = x.Employee.Phone,
                    Gender = x.Employee.Gender,
                    LeaveCredit = x.Employee.LeaveCredit,
                    VacationCredit = x.Employee.VacationCredit,
                    DepartmentName = x.Employee.Department.Name
                }

            }).ToListAsync();
        }

        /// <summary>
        /// Find a specific ticket in database using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ticket> Find(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        // *****************************************************************

        // Date related functions
        #region DATE SERVICES

        /// <summary>
        /// Get all tickets of a specific employee in a specific date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<TicketDTO>> GetAllTicketsInADateForEmployee(int id, int year, int month)
        {
            if (month == 0)
            {
                return await _context.Tickets.Where(x => x.Employee.ID == id & x.Date.Year == year)
                    .Select(x => new TicketDTO()
                    {
                        ID = x.ID,
                        EmployeeName = x.Employee.Name,
                        Type = x.Type.ToString(),
                        Date = x.Date,
                        Status = x.Status.ToString(),
                        Comment = x.Comment,
                        Employee = new EmployeeDTO
                        {
                            ID = x.Employee.ID,
                            DepartmentID = x.Employee.DepartmentID,
                            Name = x.Employee.Name,
                            Level = x.Employee.Level.ToString(),
                            Age = x.Employee.Age,
                            Email = x.Employee.Email,
                            Phone = x.Employee.Phone,
                            Gender = x.Employee.Gender,
                            LeaveCredit = x.Employee.LeaveCredit,
                            VacationCredit = x.Employee.VacationCredit,
                            DepartmentName = x.Employee.Department.Name
                        }

                    }).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("wrong input, only months between 1-12 are accepted!");
            }
            else
            {
                return await _context.Tickets.Where(x => x.Employee.ID == id & x.Date.Year == year & x.Date.Month == month)
                    .Select(x => new TicketDTO()
                    {
                        ID = x.ID,
                        EmployeeName = x.Employee.Name,
                        Type = x.Type.ToString(),
                        Date = x.Date,
                        Status = x.Status.ToString(),
                        Comment = x.Comment,
                        Employee = new EmployeeDTO
                        {
                            ID = x.Employee.ID,
                            DepartmentID = x.Employee.DepartmentID,
                            Name = x.Employee.Name,
                            Level = x.Employee.Level.ToString(),
                            Age = x.Employee.Age,
                            Email = x.Employee.Email,
                            Phone = x.Employee.Phone,
                            Gender = x.Employee.Gender,
                            LeaveCredit = x.Employee.LeaveCredit,
                            VacationCredit = x.Employee.VacationCredit,
                            DepartmentName = x.Employee.Department.Name
                        }
                    }).ToListAsync();
            }
        }


        /// <summary>
        /// Get all tickets of all employees in a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<TicketDTO>> GetAllTicketsInADate(int year, int month)
        {
            if (month == 0)
            {
                return await _context.Tickets.Where(x => x.Date.Year == year & x.Date.Month == month)
                    .Select(x => new TicketDTO()
                    {
                        ID = x.ID,
                        EmployeeName = x.Employee.Name,
                        Type = x.Type.ToString(),
                        Date = x.Date,
                        Status = x.Status.ToString(),
                        Comment = x.Comment,
                        Employee = new EmployeeDTO
                        {
                            ID = x.Employee.ID,
                            DepartmentID = x.Employee.DepartmentID,
                            Name = x.Employee.Name,
                            Level = x.Employee.Level.ToString(),
                            Age = x.Employee.Age,
                            Email = x.Employee.Email,
                            Phone = x.Employee.Phone,
                            Gender = x.Employee.Gender,
                            LeaveCredit = x.Employee.LeaveCredit,
                            VacationCredit = x.Employee.VacationCredit,
                            DepartmentName = x.Employee.Department.Name
                        }
                    }).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("wrong input, only months between 1-12 are accepted!");
            }
            else
            {
                return await _context.Tickets.Where(x => x.Date.Year == year & x.Date.Month == month)
                    .Select(x => new TicketDTO()
                    {
                        ID = x.ID,
                        EmployeeName = x.Employee.Name,
                        Type = x.Type.ToString(),
                        Date = x.Date,
                        Status = x.Status.ToString(),
                        Comment = x.Comment
                    }).ToListAsync();
            }
        }
        #endregion
    }
}