using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IEmployee
    {
        public Task<EmployeeDTO> GetEmployee(int id);
        public Task<List<EmployeeDTO>> GetEmployees();
        public Task<EmployeeDTO> AddEmployee(AddEmployeeDTO employee);
        public Task UpdateEmployee(int id, Employee employee);
        public Task DeleteEmployee(int id);
        public Task<SalarySlipDTO> GetSalarySlip(int id);
        public  Task SetEmployeeToDepartment(int empId, int departmentId);
        public Task<List<AttendanceDTO>> GetAllAttendance(int id);

        public Task<List<AttendanceDTO>> GetAllAttendancesInADateForEmployee(int id, int year, int month);

        public Task<List<AttendanceDTO>> GetAllAttendancesInADate(int year, int month);






        // Salary Slips Date part
        #region Salary Slips!!!
        // X
        public Task<List<SalarySlipDTO>> GetAllSalarySlipsInADateForEmployee(int id, int year, int month);

        // X
        public Task<List<SalarySlipDTO>> GetAllSalarySlipsInADate(int year, int month);

        #endregion



        // Tickets Date part
        #region Tickets!!

        // X
        public Task<List<TicketDTO>> GetAllTicketsInADateForEmployee(int id, int year, int month);

        // X
        public Task<List<TicketDTO>> GetAllTicketsInADate(int year, int month);


        #endregion






    }
}