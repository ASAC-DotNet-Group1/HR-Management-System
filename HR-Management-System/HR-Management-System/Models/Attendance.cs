using System;

namespace HR_Management_System.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        public string EmpName { get; set; }
        public int EmployeeID { get; set; }
        public bool StartShift { get; set; }
        public bool EndShift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Employee Employee { get; set; }
    }
}