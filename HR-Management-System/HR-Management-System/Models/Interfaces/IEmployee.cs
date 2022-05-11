using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IEmployee
    {
        public Task<Employee> GetEmployee(int id);

        public Task<List<Employee>> GetEmployees();

        public Task AddEmployee(Employee employee);

        public Task UpdateEmployee(int id, Employee employee);

        public Task DeleteEmployee(int id);
    }
}
