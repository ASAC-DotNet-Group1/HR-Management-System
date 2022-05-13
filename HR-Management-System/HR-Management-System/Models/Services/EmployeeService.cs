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

        public EmployeeService(HR_DbContext context)
        {
            _context = context;
            //starter();
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
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    Level = x.Level,
                    Attendances = x.Attendances.Select(x => new AttendanceDTO
                    {
                        Date = x.Date,
                        EmployeeID = x.EmployeeID,
                        Present = x.Present,
                    }).ToList(),
                    SalarySlip = x.SalarySlip.Select(x => new SalarySlipDTO
                    {
                        Date = x.Date,
                        EmployeeID = x.EmployeeID,
                        Ticket = x.Ticket.Select(x => new TicketDTO
                        {
                            ID = x.ID,
                            Approval = x.Approval,
                            Comment = x.Comment,
                            Date = x.Date,
                            Type = x.Type
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
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    Level = x.Level,
                    Attendances = x.Attendances.Select(x => new AttendanceDTO
                    {
                        Date = x.Date,
                        EmployeeID = x.EmployeeID,
                        Present = x.Present,
                    }).ToList(),
                    SalarySlip = x.SalarySlip.Select(x => new SalarySlipDTO
                    {
                        Date = x.Date,
                        EmployeeID = x.EmployeeID,
                        Ticket = x.Ticket.Select(x => new TicketDTO
                        {
                            ID = x.ID,
                            Approval = x.Approval,
                            Comment = x.Comment,
                            Date = x.Date,
                            Type = x.Type
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
            Department oldDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.ID == employee.DepartmentID);
            Department newDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.ID == departmentId);
            oldDepartment.Employees.Remove(employee);
            employee.DepartmentID = departmentId;
            employee.Department = newDepartment;
            _context.Entry(employee).State = EntityState.Modified;
            _context.Entry(oldDepartment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            newDepartment.Employees.Add(employee);
            _context.Entry(newDepartment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<SalarySlipDTO> GetSalarySlip(int id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.ID == id);
            DateTime Date = DateTime.Now.ToLocalTime();
            List<Ticket> tickets = _context.Tickets.Select(x => x)
                .Where(x => x.emp_id == id)
                .Where(x => x.Date.Month == Date.Month && x.Date.Day <= Date.Day).ToList();
            List<Attendance> attendances = _context.Attendances.Select(x =>x)
                .Where(x => x.EmployeeID == id)
                .Where(x => x.Date.Month == Date.Month && x.Date.Day <= Date.Day)
                .Where(x => x.Present == false).ToList();
            int totalTicket = 0;
            foreach (Ticket ticket in tickets) totalTicket += (int)ticket.Type;
            int totalAttendance = attendances.Count*20;
            return new SalarySlipDTO()
            { 
                Attendances = attendances.Select(x => new AttendanceDTO()
                {
                    Date = x.Date,
                    EmployeeID = x.EmployeeID,
                    Present = x.Present,
                }
                ).ToList(),
                Date = Date,
                Total = employee.Department.BaseSalary - totalAttendance + totalTicket,
                Ticket = tickets.Select(x => new TicketDTO() 
                {
                    Approval = x.Approval,
                    Comment = x.Comment,
                    Date = x.Date,
                    ID = x.ID,
                    Type = x.Type,
                }).ToList(),
                EmployeeID = id,
                Employee = new EmployeeDTO() 
                { 
                    DepartmentID = employee.DepartmentID,
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
                    DepartmentID = x.DepartmentID,
                }).ToList()
            };
        }
        //public void starter ()
        //{
        //    Timer timer = new Timer(1000);
        //    timer.AutoReset = true;
        //    timer.Elapsed += new ElapsedEventHandler(CheckAttendance);
        //    timer.Start();
            

        //}
        //async void CheckAttendance(object sender, ElapsedEventArgs e)
        //{
        //    DateTime Time = DateTime.Now;
        //    if (Time.Hour == 4 && Time.Minute == 17)
        //    {
        //        List<Employee> employees = await _context.Employees.ToListAsync();
        //        foreach (Employee employee in employees)
        //        {
        //            if (employee.Attendances.Last().Date.Date != Time.Date)
        //            {
        //                Attendance attendance = new Attendance()
        //                {
        //                    Date = Time,
        //                    Present = false,
        //                    EmployeeID = employee.ID,
        //                    Employee = employee,
        //                };
        //                employee.Attendances.Add(attendance);
        //                _context.Entry(employee).State = EntityState.Modified;
        //                _context.Entry(attendance).State = EntityState.Added;
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //    }


        //}

    }
}
