using System;

namespace HR_Management_System.Models.DTOs
{
    public class AttendanceDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime StartShift { get; set; }
        public DateTime EndShift { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}