using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public bool Present { get; set; }
        public DateTime Date { get; set; }
    }
}
