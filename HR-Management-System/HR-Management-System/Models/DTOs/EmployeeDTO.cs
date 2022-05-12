
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public List<EmployeeAttendance> EmployeesAttendances { get; set; }
        public List<SalarySlip> SalarySlip { get; set; }
    }
}
