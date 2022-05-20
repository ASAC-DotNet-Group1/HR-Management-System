using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface ISalarySlip
    {
        public Task<List<ShortenedSalarySlipDTO>> GetSalarySlips();
        public Task AddSalarySlip(int id);
        public Task UpdateSalarySlip(int id, SalarySlip salarySlip);
        public Task DeleteSalarySlip(int id);
        public Task<SalarySlip> Find(int id);
        public Task<double> CalculateSalary(int id, int month, int year);
        public void Test2();
        // Salary Slips Date part
        #region Salary Slips!!!
        // X
        public Task<List<SalarySlipDTO>> GetAllSalarySlipsInADateForEmployee(int id, int year, int month);

        // X
        public Task<List<ShortenedSalarySlipDTO>> GetAllSalarySlipsInADate(int year, int month);

        #endregion
    }
}
