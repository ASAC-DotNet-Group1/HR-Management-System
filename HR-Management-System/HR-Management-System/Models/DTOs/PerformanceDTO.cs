using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.DTOs
{
    public class PerformanceDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int Commitment { get; set; }
        public int Efficiency { get; set; }
        public int Communication { get; set; }
        public int TimeManagement { get; set; }
        public int QualityOfWork { get; set; }
        public double Overall { get; set; }
        public DateTime PerformanceDate { get; set; }
    }
}