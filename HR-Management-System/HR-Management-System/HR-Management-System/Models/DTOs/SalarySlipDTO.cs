using System;
using System.Collections.Generic;

namespace HR_Management_System.Models.DTOs
{
    public class SalarySlipDTO
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public EmployeeDTO Employee { get; set; }
        public List<TicketDTO> Ticket { get; set; }
        public List<AttendanceDTO> Attendances { get; set; }
    }
}
