using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public class SalarySlip
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public Employee Employee { get; set; }
        public List<Ticket> Ticket { get; set; }
        public List<EmployeeAttendance> EmployeeAttendance { get; set; }
    }
}
