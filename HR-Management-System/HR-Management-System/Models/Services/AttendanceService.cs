using HR_Management_System.Data;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.Data.SqlClient;
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

        public async Task<AttendanceDTO> AddAttendance(AttendanceDTO attendancedto)
        {
            try
            {
                Attendance attendance = new Attendance
                {
                    EmployeeID = attendancedto.EmployeeID,
                    Present = attendancedto.Present,
                    Date = System.DateTime.Now.ToLocalTime()
                };
                _context.Entry(attendance).State = EntityState.Added;

                await _context.SaveChangesAsync();
            }
            catch (SqlException exception)
            {
                if (exception.Number == 2601) // Cannot insert duplicate key row in object error
                {
                    throw new Exception();
                }
            }
            return attendancedto;
        }

        public async Task DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) { throw new Exception("The attendance with this ID i snot available!"); }
            _context.Entry(attendance).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<AttendanceDTO> GetAttendance(int id)
        {
            var attendance =  await _context.Attendances.FindAsync(id);
            if (attendance == null) { throw new Exception("The attendance with this ID i snot available!"); }
            AttendanceDTO attendancedto = new AttendanceDTO
            {
                EmployeeID = attendance.EmployeeID,
                Present = attendance.Present,
                Date = attendance.Date
            };
            return attendancedto;
        }

        public async Task<List<AttendanceDTO>> GetAttendances()
        {
            return await _context.Attendances.Select(x => new AttendanceDTO
            {
                EmployeeID = x.EmployeeID,
                Present = x.Present,
                Date = System.DateTime.Now.ToLocalTime()
            }).ToListAsync();
        }

        public async Task UpdateAttendance(int id, Attendance attendance)
        {
            _context.Entry(attendance).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
        
}
