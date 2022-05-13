using System.Collections.Generic;

namespace HR_Management_System.Models.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public List<AttendanceDTO> Attendances { get; set; }
        public List<SalarySlipDTO> SalarySlip { get; set; }
        
    }
}
