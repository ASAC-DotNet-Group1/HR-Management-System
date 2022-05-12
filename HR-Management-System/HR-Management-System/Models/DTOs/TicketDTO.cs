using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.DTOs
{
    public class TicketDTO
    {
        public int ID { get; set; }
        public Type Type { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public bool Approval { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}
