using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
