using System.Collections.Generic;

namespace HR_Management_System.Models.DTOs
{
    public class DepartmentDTO
    {
        public string Name { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}
