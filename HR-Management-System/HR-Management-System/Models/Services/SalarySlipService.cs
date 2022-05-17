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
    public class SalarySlipService : ISalarySlip
    {
        private readonly HR_DbContext _context;

        public SalarySlipService(HR_DbContext context)
        {
            _context = context;
        }
        public void Test2()
        {
            Console.WriteLine("This run month");
        }
        public async Task GenerateSlary()
        {
            List<int> employees = await _context.Employees.Select(x => x.ID).ToListAsync();
            foreach (int id in employees)
            {
                await AddSalarySlip(id);
            }
        }


        /// <summary>
        /// Calculate a salary of a specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<double> CalculateSalary(int id, int month, int year)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            List<Attendance> attendances = await _context.Attendances.Where(x => x.EmployeeID == id && x.StartShift == false
            && x.StartDate.Month == month && x.StartDate.Year == year).ToListAsync();
            List<Ticket> tickets = await _context.Tickets.Where(x => x.EmployeeID == id && x.Status == Status.Approved && x.Date.Month == month && x.Date.Year == year).ToListAsync();

            double totalTickets = 0;
            foreach (Ticket ticket in tickets) totalTickets += ticket.Total;
            return attendances.Count + totalTickets;
        }

        /// <summary>
        /// Create a new salary slip of a specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task AddSalarySlip(int id)
        {
            DateTime dateTime = DateTime.Now.ToLocalTime();
            int month = dateTime.Month;
            int year = dateTime.Year;

            month--;
            if (month == 0)
            {
                month = 12;
                year--;
            }

            SalarySlip salarySlip = new SalarySlip()
            {
                EmployeeID = id,
                Date = dateTime,
                Total = await CalculateSalary(id, month, year)
            };
            _context.Entry(salarySlip).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Delete salary slip using a salary slip ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSalarySlip(int id)
        {
            SalarySlip SalarySlips = await _context.SalarySlips.FindAsync(id);

            _context.Entry(SalarySlips).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }


       

        /// <summary>
        /// Get all salary slips
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShortenedSalarySlipDTO>> GetSalarySlips()
        {
            List<Attendance> attendances = await _context.Attendances.ToListAsync();
            List<Ticket> tickets = await _context.Tickets.ToListAsync();
            return await _context.SalarySlips.Select(slip => new ShortenedSalarySlipDTO()
            {
                Date = slip.Date,
                EmployeeID = slip.EmployeeID,
                EmpName = slip.Employee.Name,
                Total = slip.Total,
            }).ToListAsync();
        }

        // CHECK THIS
        public async Task UpdateSalarySlip(int id, SalarySlip salarySlip) //////////////
        {
            _context.Entry(salarySlip).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Find a salary slip using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SalarySlip> Find(int id)
        {
            return await _context.SalarySlips.FindAsync(id);
        }

        /// <summary>
        /// Get all salary slips of a specific employee in a specific Date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<SalarySlipDTO>> GetAllSalarySlipsInADateForEmployee(int id, int year, int month)
        {

            if (month == 0)
            {
                return await _context.SalarySlips.Where(x => x.EmployeeID == id & x.Date.Year == year).Select(x => new SalarySlipDTO()
                {
                    EmployeeID = x.EmployeeID,
                    Date = x.Date,
                    Total = x.Total,
                }).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("wrong input, only months between 1-12 are accepted!");
            }
            else
            {
                return await _context.SalarySlips.Where(x => x.EmployeeID == id & x.Date.Year == year & x.Date.Month == month).Select(x => new SalarySlipDTO()
                {
                    Date = x.Date,
                    EmployeeID = x.EmployeeID,
                    Employee = new EmployeeDTO()
                    {
                        ID = id,
                        Level = x.Employee.Level.ToString(),
                        Name = x.Employee.Name,
                        DepartmentID = x.Employee.DepartmentID,
                    },
                    Attendances = x.Attendances.Select(x => new AttendanceDTO()
                    {
                        EndShift = x.EndDate,
                        EmployeeID = x.EmployeeID,
                        StartShift = x.StartDate,
                        Name = x.EmpName,
                    }).ToList(),
                    Ticket = x.Ticket.Select(x => new TicketDTO()
                    {
                        ID = x.ID,
                        Status = x.Status.ToString(),
                        Comment = x.Comment,
                        Date = x.Date,
                        Type = x.Type.ToString(),
                    }).ToList(),
                    Total = x.Total,
                }).ToListAsync();
            }




        }


        /// <summary>
        /// Get all salary slips of all employees in a specific Date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ShortenedSalarySlipDTO>> GetAllSalarySlipsInADate(int year, int month)
        {

            if (month == 0)
            {
                return await _context.SalarySlips.Where(x => x.Date.Year == year).Select(x => new ShortenedSalarySlipDTO()
                {
                    EmployeeID = x.EmployeeID,
                    EmpName = x.Employee.Name,
                    Date = x.Date,
                    Total = x.Total
                }).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("wrong input, only months between 1-12 are accepted!");
            }
            else
            {
                return await _context.SalarySlips.Where(x => x.Date.Year == year & x.Date.Month == month).Select(x => new ShortenedSalarySlipDTO()
                {
                    EmployeeID = x.EmployeeID,
                    EmpName = x.Employee.Name,
                    Date = x.Date,
                    Total = x.Total
                }).ToListAsync();
            }

        }


    }



}