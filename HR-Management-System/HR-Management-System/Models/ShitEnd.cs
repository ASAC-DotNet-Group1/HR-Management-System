using System;

namespace HR_Management_System.Models
{
    public class ShiftEnd
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public bool Left { get; set; }
        public DateTime Date { get; set; }
        public Employee Employee { get; set; }
    }
}
