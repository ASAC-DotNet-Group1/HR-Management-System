using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.DTOs
{
    public class AttendanceDTO 
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
