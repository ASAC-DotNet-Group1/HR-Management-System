using System;

namespace HR_Management_System.Models.DTOs
{
    public class TicketDTO
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Level { get; set; }
        public Type Type { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }

    }
}
