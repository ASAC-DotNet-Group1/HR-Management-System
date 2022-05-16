using HR_Management_System.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IPerformance
    {
        public Task<List<PerformanceDTO>> GetAllPerformanceReports();
        public Task<PerformanceDTO> GetPerformanceReport(int id);
        public Task<PerformanceDTO> AddPerformance(AddPerformanceDTO performance);
        public Task<List<PerformanceDTO>> EmployeePerformanceReports(int id);
        public Task<List<PerformanceDTO>> PerformanceReportsForDepartment(int id);
        public Task<List<PerformanceDTO>> PerformanceReportsInSpecificMonth(int year, int month);
        public Task<List<PerformanceDTO>> PerformanceReportsForEmployeeInSpecificMonth(int id, int year, int month);
        public Task<PerformanceDTO> UpdatePerformance(int id, UpdatePerformanceDTO performance);
        public Task DeletePerformance(int id);
    }
}