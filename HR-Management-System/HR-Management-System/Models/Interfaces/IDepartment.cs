using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IDepartment
    {
        public Task<DepartmentDTO> GetDepartment(int id);
        public Task<List<DepartmentDTO>> GetDepartments();
        public Task AddDepartment(Department department);
        public Task UpdateDepartment(int id, Department department);
        public Task DeleteDepartment(int id);
    }
}