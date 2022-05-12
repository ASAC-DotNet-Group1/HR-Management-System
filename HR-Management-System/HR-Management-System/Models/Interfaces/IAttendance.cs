using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IAttendance
    {
        public Task<AttendanceDTO> GetAttendance(int id);

        public Task<List<AttendanceDTO>> GetAttendances();
        
        public Task<AttendanceDTO> AddAttendance(AttendanceDTO attendance);

        public Task UpdateAttendance(int id, Attendance attendance);

        public Task DeleteAttendance(int id);
    }
}
