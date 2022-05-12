using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public class EmployeeAttendance
    {
        public int AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public Attendance Attendance { get; set; }
    }
}
