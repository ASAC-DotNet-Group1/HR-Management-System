using System.Collections.Generic;

namespace HR_Management_System.Models.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public List<AttendanceDTO> Attendances { get; set; }
        public List<SalarySlipDTO> SalarySlips { get; set; }
        public List<TicketDTO> Tickets { get; set; }
        
    }
}