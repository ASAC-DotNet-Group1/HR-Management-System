﻿using System.Collections.Generic;

namespace HR_Management_System.Models
{
    public enum Level
    {
        Junior = 1,
        MidSenior,
        Senior
    }

    public class Employee
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Salary { get; set; }
        public Level Level { get; set; }
        public List<Ticket> Ticket { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<SalarySlip> SalarySlip { get; set; }
        public Department Department { get; set; }
    }

}
