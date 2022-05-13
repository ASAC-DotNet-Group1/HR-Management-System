using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface ISalarySlip
    {
        public Task<SalarySlip> GetSalarySlip(int id);

        public Task<List<SalarySlip>> GetSalarySlips();

        public Task AddSalarySlip(SalarySlip salarySlip);

        public Task UpdateSalarySlip(int id, SalarySlip salarySlip);

        public Task DeleteSalarySlip(int id);
    }

}
