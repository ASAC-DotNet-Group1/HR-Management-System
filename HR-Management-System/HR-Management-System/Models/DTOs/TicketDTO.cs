using System;

namespace HR_Management_System.Models.DTOs
{
    public class TicketDTO
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}
