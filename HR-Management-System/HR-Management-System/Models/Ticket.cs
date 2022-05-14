using System;

namespace HR_Management_System.Models
{
    public enum Type
    {
        Vacation,
        Leave,
        Overtime
    }

    public enum Status
    {
        Pending,
        Approved,
        Denied
    }
    public class Ticket
    {
        public int ID { get; set; }
        public int emp_id { get; set; }
        public Type Type { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Employee Employee { get; set; }
    }
}
