using System;

namespace HR_Management_System.Models.DTOs
{
    public class ShiftEndDTO
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public bool Left { get; set; }


    }
}
