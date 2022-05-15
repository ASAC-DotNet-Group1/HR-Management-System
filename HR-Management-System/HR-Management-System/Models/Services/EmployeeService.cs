using HR_Management_System.Data;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace HR_Management_System.Models.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly HR_DbContext _context;
        private readonly ISalarySlip _salarySlip;

        public EmployeeService(HR_DbContext context, ISalarySlip salarySlip)
        {
            _context = context;
            _salarySlip = salarySlip;
        }

        public async Task AddEmployee(Employee employee)
        {
            Department department = await _context.Departments.FindAsync(employee.DepartmentID);
            employee.Department = department;
            _context.Entry(employee).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);

            _context.Entry(employee).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            return await _context.Employees
                .Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    DepartmentName = x.Department.Name,
                    Name = x.Name,
                    Level = x.Level,
                    Attendances = x.Attendances.Select(x => new AttendanceDTO
                    {
                        Name = x.Employee.Name,
                        StartShift = x.StartDate,
                        EmployeeID = x.EmployeeID,
                        EndShift = x.EndDate,
                        
                    }).ToList(),
                    SalarySlips = x.SalarySlips.Select(x => new SalarySlipDTO
                    {
                        Date = x.Date,
                        EmployeeID = x.EmployeeID,
                        Ticket = x.Ticket.Select(x => new TicketDTO
                        {
                            ID = x.ID,
                            Status=x.Status,
                            Comment = x.Comment,
                            Date = x.Date,
                            Type = x.Type.ToString()
                        }).ToList(),
                        Total = x.Total,
                    }
                    ).ToList(),
                }).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            return await _context.Employees
                .Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    DepartmentName = x.Department.Name,
                    Name = x.Name,
                    Level = x.Level,
                    Attendances = x.Attendances.Select(x => new AttendanceDTO
                    {
                        StartShift = x.StartDate,
                        EmployeeID = x.EmployeeID,
                        EndShift = x.EndDate,
                        Name = x.EmpName,
                    }).ToList(),
                    SalarySlips = x.SalarySlips.Select(x => new SalarySlipDTO
                    {
                        Date = x.Date,
                        EmployeeID = x.EmployeeID,
                        Ticket = x.Ticket.Select(x => new TicketDTO
                        {
                            ID = x.ID,
                            Status = x.Status,
                            Comment = x.Comment,
                            Date = x.Date,
                            Type = x.Type.ToString(),
                            
                        }).ToList(),
                        Total = x.Total,
                    }
                    ).ToList(),
                }).ToListAsync();
        }

        public async Task UpdateEmployee(int id, Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task SetEmployeeToDepartment(int empId, int departmentId)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.ID == empId);
            employee.DepartmentID = departmentId;
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<SalarySlipDTO> GetSalarySlip(int id)
        {
            DateTime Date = DateTime.Now.ToLocalTime();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.ID == id);

            List<Ticket> tickets = _context.Tickets.Select(x => x)
                .Where(x => x.Emp_id == id)
                .Where(x => x.Date.Month == Date.Month && x.Date.Day <= Date.Day).ToList(); // we use day prop in case some dumb like osama create a ticket with future Date

            List<Attendance> attendances = _context.Attendances.Select(x => x)
                .Where(x => x.EmployeeID == id)
                .Where(x => x.StartDate.Month == Date.Month && x.StartDate.Day <= Date.Day)
                .Where(x => x.StartShift == false).ToList();

            double total = await _salarySlip.CalculateSalary(employee.ID, Date);
            return new SalarySlipDTO()
            {
                Attendances = attendances.Select(x => new AttendanceDTO()
                {
                    StartShift = x.StartDate,
                    EmployeeID = x.EmployeeID,
                    EndShift = x.EndDate,
                    Name = x.EmpName
                }
                ).ToList(),
                Date = Date,
                Total = total,
                Ticket = tickets.Select(x => new TicketDTO()
                {
                    Status = x.Status,
                    Comment = x.Comment,
                    Date = x.Date,
                    ID = x.ID,
                    Type = x.Type.ToString(),
                }).ToList(),
                EmployeeID = id,
                Employee = new EmployeeDTO()
                {
                    DepartmentName = employee.Department.Name,
                    ID = employee.ID,
                    Level = employee.Level,
                    Name = employee.Name,

                }
            };

        }

        public async Task<DepartmentDTO> GetDepartmentForEmployee(int id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.ID == id);
            Department department = await _context.Departments.FirstOrDefaultAsync(x => x.ID == employee.DepartmentID);
            return new DepartmentDTO()
            {
                Name = department.Name,
                Employees = department.Employees.Select(x => new EmployeeDTO()
                {
                    Name = x.Name,
                    Level = x.Level,
                    DepartmentName = x.Department.Name,
                }).ToList()
            };
        }

        /// <summary>
        /// Get all attendances for a specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<AttendanceDTO>> GetAllAttendance(int id)
        {

            return await _context.Attendances.Select(x => new AttendanceDTO()
            {
                EmployeeID = x.EmployeeID,
                StartShift = x.StartDate,
                EndShift = x.EndDate
                
            }).Where(x => x.EmployeeID == id).ToListAsync();
        }

        public async Task<List<AttendanceDTO>> GetAllAttendancesInADateForEmployee(int id, int year, int month)
        {
            //if month is inputted as 0, then give me the whole year
            if (month == 0)
            {
                return await _context.Attendances.Select(x => new AttendanceDTO()
                {
                    Name = x.EmpName,
                    EmployeeID = x.EmployeeID,
                    StartShift = x.StartDate,
                    EndShift= x.EndDate
                }).Where(x => x.EmployeeID == id & x.StartShift.Year == year).ToListAsync();
            }

            else if (month > 12)
            {
                throw new Exception("Wrong input, only months between 1-12 are accepted");
            }

            else
            {
                return await _context.Attendances.Select(x => new AttendanceDTO()
                {
                    Name = x.EmpName,
                    EmployeeID = x.EmployeeID,
                    StartShift = x.StartDate,
                    EndShift = x.EndDate
                }).Where(x => x.EmployeeID == id & x.StartShift.Year == year & x.StartShift.Month == month).ToListAsync();
            }
        }

        public async Task<List<AttendanceDTO>> GetAllAttendancesInADate(int year, int month)
        {
            //if month is inputted as 0, then give me the whole year
            if (month == 0)
            {
                return await _context.Attendances.Select(x => new AttendanceDTO()
                {
                    Name = x.EmpName,
                    EmployeeID = x.EmployeeID,
                    StartShift = x.StartDate,
                    EndShift = x.EndDate
                }).Where(x => x.StartShift.Year == year).ToListAsync();
            }

            else if (month > 12 || month < 0)
            {
                throw new Exception("Wrong input, only months between 1-12 are accepted");
            }

            else
            {
                return await _context.Attendances.Select(x => new AttendanceDTO()
                {
                    Name = x.EmpName,
                    EmployeeID = x.EmployeeID,
                    StartShift = x.StartDate,
                    EndShift = x.EndDate
                }).Where(x => x.StartShift.Year == year & x.StartShift.Month == month).ToListAsync();
            }
        }

        


    }

}