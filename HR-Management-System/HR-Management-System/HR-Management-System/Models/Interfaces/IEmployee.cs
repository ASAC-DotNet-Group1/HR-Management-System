using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IEmployee
    {
        public Task<EmployeeDTO> GetEmployee(int id);

        public Task<List<EmployeeDTO>> GetEmployees();

        public Task AddEmployee(Employee employee);

        public Task UpdateEmployee(int id, Employee employee);

        public Task DeleteEmployee(int id);
        public Task<SalarySlipDTO> GetSalarySlip(int id);
        public Task<DepartmentDTO> GetDepartmentForEmployee(int id);
        public  Task SetEmployeeToDepartment(int empId, int departmentId);
    }
}
