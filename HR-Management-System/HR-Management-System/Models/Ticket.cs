using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public enum Type
    {
        Loan = 1000,
        Leave = -10,
        Overtime = 20
    }
    public class Ticket
    {
        public int ID { get; set; }
        public int emp_id { get; set; }
        public Type Type { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public bool Approval { get; set; }
        public Employee Employee { get; set; }
    }
}
