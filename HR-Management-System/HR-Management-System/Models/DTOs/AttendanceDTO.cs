using System;

namespace HR_Management_System.Models.DTOs
{
    public class AttendanceDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool Present { get; set; }

    }
}
