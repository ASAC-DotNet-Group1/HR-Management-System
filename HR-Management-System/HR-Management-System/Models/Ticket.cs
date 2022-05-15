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
        public int Emp_id { get; set; }
        public string EmpName { get; set; }
        public Type Type { get; set; }
        public string Comment { get; set; }
        public int Amount { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Employee Employee { get; set; }
    }
}
