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
            return await _context.Attendances.Where(x => x.ID == id).Select(x => new AttendanceDTO
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

        public async Task Arrival(AttendanceDTO attendancedto)
        {
            int empID = await _context.Employees.Where(x => x.ID == attendancedto.EmployeeID).Select(x => x.ID).FirstOrDefaultAsync();
            if(empID != 0)
            {
                Attendance attendance = new Attendance
                {
                    EmployeeID = attendancedto.EmployeeID,
                    StartShift = true,
                    StartDate = System.DateTime.Now.ToLocalTime()
                };

                _context.Entry(attendance).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Employee was not found");
            }
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