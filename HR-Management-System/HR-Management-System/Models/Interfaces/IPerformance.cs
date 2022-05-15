using HR_Management_System.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    interface IPerformance
    {
        public Task AddPerformance(Performance performance);
        public Task<List<PerformanceDTO>> GetAllPerformanceReports();
        public Task<List<PerformanceDTO>> EmployeePerformanceReports(int id);
        public Task<List<PerformanceDTO>> PerformanceReportsForDepartment(string name);
        public Task<List<PerformanceDTO>> PerformanceReportsInSpecificMonth(int year, int month);
        public Task<List<PerformanceDTO>> PerformanceReportsForEmployeeInSpecificMonth(int id, int year, int month);
        public Task UpdatePerformance(int id, Performance performance);
        public Task DeletePerformance(int id);
    }
}
