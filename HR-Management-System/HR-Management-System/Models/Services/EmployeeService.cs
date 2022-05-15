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
    public class EmployeeService : IEmployee
    {
        private readonly HR_DbContext _context;
        private readonly ISalarySlip _salarySlip;

        public EmployeeService(HR_DbContext context, ISalarySlip salarySlip)
        {
            _context = context;
            _salarySlip = salarySlip;
        }

        public async Task<EmployeeDTO> AddEmployee(AddEmployeeDTO newEmployee)
        {
            Employee employee = new Employee
            {
                Name = newEmployee.Name,
                DepartmentID = newEmployee.DepartmentID,
                Age = newEmployee.Age,
                Email = newEmployee.Email,
                Gender = newEmployee.Gender,
                Level = newEmployee.Level,
                Password = newEmployee.Password,
                Phone = newEmployee.Phone,
                Salary = newEmployee.Salary,
                LeaveCredit = 14,
                VacationCredit = 14,
            };
            _context.Entry(employee).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new EmployeeDTO
            {
                ID = employee.ID,
                Age = employee.Age,
                DepartmentID = employee.DepartmentID,
                Email = employee.Email,
                Gender = employee.Gender,
                LeaveCredit = employee.LeaveCredit,
                Level = employee.Level.ToString(),
                Name = employee.Name,
                Phone = employee.Phone,
                VacationCredit = employee.VacationCredit,
                Department = employee.Department,
            };
        }

        public async Task DeleteEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            _context.Entry(employee).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            return await _context.Employees
                .Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    Level = x.Level.ToString(),
                    Age = x.Age,
                    Email = x.Email,
                    Phone = x.Phone,
                    Gender = x.Gender,
                    LeaveCredit = x.LeaveCredit,
                    VacationCredit = x.VacationCredit,
                    Department = x.Department,
                }).FirstAsync();
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {

            return await _context.Employees
                .Select(x => new EmployeeDTO
                {
                    ID = x.ID,
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    Level = x.Level.ToString(),
                    Age = x.Age,
                    Email = x.Email,
                    Phone = x.Phone,
                    Gender = x.Gender,
                    LeaveCredit = x.LeaveCredit,
                    VacationCredit = x.VacationCredit,
                    Department = x.Department
                }).ToListAsync();
        }

        public async Task UpdateEmployee(int id, Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task SetEmployeeToDepartment(int empId, int departmentId)
        {
            Employee employee = await _context.Employees.FindAsync(empId);
            employee.DepartmentID = departmentId;
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<SalarySlipDTO> GetSalarySlip(int id)
        {
            DateTime Date = DateTime.Now.ToLocalTime();
            Employee employee = await _context.Employees.FindAsync(id);

            List<Ticket> tickets = _context.Tickets
                .Where(x => x.EmployeeID == id)
                .Where(x => x.Date.Month == Date.Month && x.Date.Day <= Date.Day).ToList(); // we use day prop in case some dumb like osama create a ticket with future Date

            List<Attendance> attendances = _context.Attendances
                .Where(x => x.EmployeeID == id)
                .Where(x => x.StartDate.Month == Date.Month && x.StartDate.Day <= Date.Day)
                .Where(x => x.StartShift == false).ToList();

            double total = await _salarySlip.CalculateSalary(employee.ID, Date);
            return new SalarySlipDTO()
            {
                EmployeeID = id,
                Date = Date,
                Total = total,

                Attendances = attendances.Select(x => new AttendanceDTO()
                {
                    StartShift = x.StartDate,
                    EmployeeID = x.EmployeeID,
                    EndShift = x.EndDate,
                    Name = x.EmpName
                }
                ).ToList(),
                Ticket = tickets.Where(x => x.Status == Status.Approved).Select(x => new TicketDTO()
                {
                    
                    Status = x.Status.ToString(),
                    Comment = x.Comment,
                    Date = x.Date,
                    ID = x.ID,
                    Type = x.Type.ToString(),
                }).ToList(),
            };
        }

        /// <summary>
        /// Get all attendances for a specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<AttendanceDTO>> GetAllAttendance(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            return await _context.Attendances.Where(x => x.EmployeeID == id).Select(x => new AttendanceDTO()
            {
                EmployeeID = x.EmployeeID,
                StartShift = x.StartDate,
                EndShift = x.EndDate

            }).ToListAsync();
        }
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

            }).ToListAsync();
        }

        public async Task<List<AttendanceDTO>> GetAllAttendancesInADateForEmployee(int id, int year, int month)
        {
            //if month is inputted as 0, then give me the whole year
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Unvalid Employee ID");
            if (month == 0)
            {
                return await _context.Attendances.Select(x => new AttendanceDTO()
                {
                    Name = x.EmpName,
                    EmployeeID = x.EmployeeID,
                    StartShift = x.StartDate,
                    EndShift = x.EndDate,
                }).Where(x => x.EmployeeID == id & x.StartShift.Year == year).ToListAsync();
            }

            else if (month > 12 && month < 0)
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
                    EndShift = x.EndDate,
                    Employee = x.Employee
                }).Where(x => x.StartShift.Year == year).ToListAsync();
            }

            else if (month > 12 && month < 0)
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
                    EndShift = x.EndDate,
                    Employee = x.Employee
                }).Where(x => x.StartShift.Year == year & x.StartShift.Month == month).ToListAsync();
            }
        }




    }

}