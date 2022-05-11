using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public class SalarySlip
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int EmployeeID { get; set; }
        public int TicketID { get; set; }
        public int EmpAttendID { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}
