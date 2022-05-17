using System;

namespace HR_Management_System.Models.DTOs
{
    public class ShortenedSalarySlipDTO
    {
        public int EmployeeID { get; set; }
        public string EmpName { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        
    }
}
