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
    public class AttendanceService : IAttendance
    {
        private readonly HR_DbContext _context;

        public AttendanceService(HR_DbContext context)
        {
            _context = context;
        }
        public  void Test1() 
        {
            Console.WriteLine("This run every day at 9:10");
        }
        public async Task TakeAttendance()
        {
            DateTime date = DateTime.Now.ToLocalTime();
            List<int> attendances = await _context.Attendances.Where(x => x.StartDate.ToString("MM-dd-yyyy") == date.ToString("MM-dd-yyyy")).Select(x => x.EmployeeID).ToListAsync();
            List<int> employees = await _context.Employees.Select(x => x.ID).ToListAsync();
            foreach(int id in employees)
            {
                if(!attendances.Contains(id))
                {
                    Attendance attendance = new Attendance()
                    {
                        EmployeeID = id,
                        EndShift = false,
                        StartShift = false,
                        StartDate = date,
                        EndDate = date,
                    };
                    _context.Entry(attendance).State = EntityState.Added;
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<AttendanceDTO>> GetAttendances()
        {
            return await _context.Attendances.Select(x => new AttendanceDTO()
            {
                EmployeeID = x.EmployeeID,
                Name = x.Employee.Name,
                StartShift = x.StartDate,
                EndShift = x.EndDate
            }).ToListAsync();
        }

        public async Task<AttendanceDTO> GetAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if(attendance == null) { throw new Exception("Attendance was not found"); }
            return await _context.Attendances.Where(x => x.ID ==id).Select(x => new AttendanceDTO()
            {
                EmployeeID = x.EmployeeID,
                Name = x.Employee.Name,
                StartShift = x.StartDate,
                EndShift = x.EndDate

            }).FirstOrDefaultAsync();
        }

        public async Task UpdateAttendance(int id, Attendance attendance)
        {
            var oldAttendance = await _context.Attendances.FindAsync(id);
            
            if(oldAttendance != null)
            {
                oldAttendance.StartShift = attendance.StartShift;
                _context.Entry(oldAttendance).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Employee was not found");
            }
        }

        public async Task Arrival(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                Attendance attendance = new Attendance
                {
                    EmpName = employee.Name,
                    EmployeeID = id,
                    StartShift = true,
                    StartDate = DateTime.Now.ToLocalTime()
                };

                _context.Entry(attendance).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Employee was not found");
            }
        }
        public async Task  Leave (int id)
        {
            DateTime date = DateTime.Now.ToLocalTime();
            Attendance attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.EmployeeID == id && x.StartDate.ToString("MM-dd-yyyy") == date.ToString("MM-dd-yyyy"));
            if (attendance == null)
                throw new Exception("You dont Start Shift for Today");
            attendance.EndDate = date;
            attendance.EndShift = true;
            _context.Entry(attendance).State= EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAttendance(int id)
        {
            Attendance attendance = await _context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                throw new Exception("Attendance was not found");
            }
            _context.Entry(attendance).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}