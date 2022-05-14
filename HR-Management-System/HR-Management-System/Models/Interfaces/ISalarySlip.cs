using HR_Management_System.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface ISalarySlip
    {
        public Task<SalarySlipDTO> GetSalarySlip(int id, int month);

        public Task<List<SalarySlipDTO>> GetSalarySlips();

        public Task AddSalarySlip(int id);

        public Task UpdateSalarySlip(int id, SalarySlip salarySlip);

        public Task DeleteSalarySlip(int id);
        public Task<SalarySlip> Find(int id);
        public double CalculateSalary(double baseSalary, List<Attendance> attendances, List<Ticket> tickets);
    }
}
