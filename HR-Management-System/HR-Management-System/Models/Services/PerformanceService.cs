using HR_Management_System.Data;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Services
{
    public class PerformanceService : IPerformance
    {
        private readonly HR_DbContext _context;

        public PerformanceService(HR_DbContext context)
        {
            _context = context;
        }

        public async Task AddPerformance(Performance performance)
        {
            if(performance.Commitment >= 0 && performance.Commitment<=10
                && performance.Efficiency >=0 && performance.Efficiency<=10
                && performance.Communication >=0 && performance.Communication<=10
                && performance.TimeManagement >=0 && performance.TimeManagement<=10
                && performance.QualityOfWork >=0 && performance.QualityOfWork<=10
                )
            {
                performance.Overall = (performance.Commitment + performance.Efficiency +
                    performance.Communication + performance.TimeManagement + performance.QualityOfWork) / 5 *100/100;
                _context.Entry(performance).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Please enter values between 0-10");
            }
            
        }

        public async Task<List<PerformanceDTO>> GetAllPerformanceReports()
        {
            return await _context.Performances.Select(x => new PerformanceDTO()
            {
                PerformanceDate = x.PerformanceDate,
                Commitment = x.Commitment,
                Communication = x.Communication,
                Efficiency = x.Efficiency,
                QualityOfWork = x.QualityOfWork,
                TimeManagement = x.TimeManagement,
                Overall = x.Overall
            }).ToListAsync();
        }

        public async Task<List<PerformanceDTO>> EmployeePerformanceReports(int id)
        {
            return await _context.Performances.Select(x => new PerformanceDTO()
            {
                PerformanceDate = x.PerformanceDate,
                Commitment = x.Commitment,
                Communication = x.Communication,
                Efficiency = x.Efficiency,
                QualityOfWork = x.QualityOfWork,
                TimeManagement = x.TimeManagement,
                Overall = x.Overall
            }).Where(x => x.Employee.ID == id).ToListAsync();
        }

        public async Task<List<PerformanceDTO>> PerformanceReportsForDepartment(string name)
        {
            return await _context.Performances.Select(x => new PerformanceDTO()
            {
                PerformanceDate = x.PerformanceDate,
                Commitment = x.Commitment,
                Communication = x.Communication,
                Efficiency = x.Efficiency,
                QualityOfWork = x.QualityOfWork,
                TimeManagement = x.TimeManagement,
                Overall = x.Overall
            }).Where(x => x.Employee.DepartmentName == name).ToListAsync();
        }

        public async Task<List<PerformanceDTO>> PerformanceReportsInSpecificMonth(int year, int month)
        {
            if (month == 0)
            {
                return await _context.Performances.Select(x => new PerformanceDTO()
                {
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).Where(x => x.PerformanceDate.Year == year).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("Wrong input, only months between 1-12 are accepted");
            }
            else
            {
                return await _context.Performances.Select(x => new PerformanceDTO()
                {
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).Where(x => x.PerformanceDate.Year == year && x.PerformanceDate.Month == month).ToListAsync();
            }
        }
        public async Task<List<PerformanceDTO>> PerformanceReportsForEmployeeInSpecificMonth(int id, int year, int month)
        {
            if (month == 0)
            {
                return await _context.Performances.Select(x => new PerformanceDTO()
                {
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).Where(x => x.Employee.ID == id && x.PerformanceDate.Year == year).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("Wrong input, only months between 1-12 are accepted");
            }
            else
            {
                return await _context.Performances.Select(x => new PerformanceDTO()
                {
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).Where(x => x.Employee.ID == id && x.PerformanceDate.Year == year && x.PerformanceDate.Month == month).ToListAsync();
            }
        }

      
        public Task UpdatePerformance(int id, Performance performance)
        {
            throw new NotImplementedException();
        }

        public Task DeletePerformance(int id)
        {
            throw new NotImplementedException();
        }
    }
}
