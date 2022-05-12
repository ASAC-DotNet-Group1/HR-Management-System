using HR_Management_System.Data;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task AddAttendance(Attendance attendance)
        {
            _context.Entry(attendance).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAttendance(int id)
        {
            Attendance attendance = await GetAttendance(id);

            _context.Entry(attendance).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<Attendance> GetAttendance(int id)
        {
            return await _context.Attendances.FindAsync(id);
        }

        public async Task<List<Attendance>> GetAttendances()
        {
            return await _context.Attendances.ToListAsync();
        }

        public async Task UpdateAttendance(int id, Attendance attendance)
        {
            _context.Entry(attendance).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
        
}
