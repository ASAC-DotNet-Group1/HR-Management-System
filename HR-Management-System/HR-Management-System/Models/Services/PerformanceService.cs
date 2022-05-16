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

        public async Task/*<PerformanceDTO>*/ AddPerformance(AddPerformanceDTO addPerformanceDTO)
        {
            
            if(addPerformanceDTO.Commitment >= 0 && addPerformanceDTO.Commitment<=10
                && addPerformanceDTO.Efficiency >=0 && addPerformanceDTO.Efficiency<=10
                && addPerformanceDTO.Communication >=0 && addPerformanceDTO.Communication<=10
                && addPerformanceDTO.TimeManagement >=0 && addPerformanceDTO.TimeManagement<=10
                && addPerformanceDTO.QualityOfWork >=0 && addPerformanceDTO.QualityOfWork<=10
                )
            {
                DateTime dateTime = DateTime.Now.ToLocalTime();
                Performance performance = new Performance
                {
                    PerformanceDate = dateTime,
                    EmployeeID = addPerformanceDTO.EmployeeID,
                    Commitment = addPerformanceDTO.Commitment,
                    Communication = addPerformanceDTO.Communication,
                    Efficiency = addPerformanceDTO.Efficiency,
                    QualityOfWork = addPerformanceDTO.QualityOfWork,
                    TimeManagement = addPerformanceDTO.TimeManagement,
                };
                performance.Overall = (addPerformanceDTO.Commitment + addPerformanceDTO.Efficiency +
                addPerformanceDTO.Communication + addPerformanceDTO.TimeManagement + addPerformanceDTO.QualityOfWork) / 5 *10;
                _context.Entry(performance).State = EntityState.Added;
                await _context.SaveChangesAsync();

                // Needs fix. 

                //performance = await _context.Performances.FirstOrDefaultAsync(x => x.EmployeeID == addPerformanceDTO.EmployeeID && x.PerformanceDate == dateTime);
                //return new PerformanceDTO
                //{
                //    EmployeeID = performance.EmployeeID,
                //    Commitment = performance.Commitment,
                //    TimeManagement = performance.TimeManagement,
                //    QualityOfWork = performance.QualityOfWork,
                //    Communication = performance.Communication,
                //    Efficiency = performance.Efficiency,
                //    Overall = performance.Overall,
                //    PerformanceDate = performance.PerformanceDate
                //};
            }
            else
            {
                throw new Exception("Please enter values between 0-10");
            }
        }
        public async Task<PerformanceDTO> GetPerformanceReport(int id)
        {
            return await _context.Performances.Where(x => x.ID == id).Select(x => new PerformanceDTO
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.Employee.Name,
                PerformanceDate = x.PerformanceDate,
                Commitment = x.Commitment,
                Communication = x.Communication,
                Efficiency = x.Efficiency,
                QualityOfWork = x.QualityOfWork,
                TimeManagement = x.TimeManagement,
                Overall = x.Overall,
                
            }).FirstOrDefaultAsync();
        }
        public async Task<List<PerformanceDTO>> GetAllPerformanceReports()
        {
            return await _context.Performances.Select(x => new PerformanceDTO()
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.Employee.Name,
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
            return await _context.Performances.Where(x => x.EmployeeID == id).Select(x => new PerformanceDTO()
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.Employee.Name,
                PerformanceDate = x.PerformanceDate,
                Commitment = x.Commitment,
                Communication = x.Communication,
                Efficiency = x.Efficiency,
                QualityOfWork = x.QualityOfWork,
                TimeManagement = x.TimeManagement,
                Overall = x.Overall
            }).ToListAsync();
        }

        public async Task<List<PerformanceDTO>> PerformanceReportsForDepartment(int id)
        {
            return await _context.Performances.Where(x => x.Employee.DepartmentID == id).Select(x => new PerformanceDTO()
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.Employee.Name,
                PerformanceDate = x.PerformanceDate,
                Commitment = x.Commitment,
                Communication = x.Communication,
                Efficiency = x.Efficiency,
                QualityOfWork = x.QualityOfWork,
                TimeManagement = x.TimeManagement,
                Overall = x.Overall,
            }).ToListAsync();
        }

        public async Task<List<PerformanceDTO>> PerformanceReportsInSpecificMonth(int year, int month)
        {
            if (month == 0)
            {
                return await _context.Performances.Where(x => x.PerformanceDate.Year == year).Select(x => new PerformanceDTO()
                {
                    EmployeeID = x.EmployeeID,
                    EmployeeName = x.Employee.Name,
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("Wrong input, only months between 1-12 are accepted");
            }
            else
            {
                return await _context.Performances.Where(x => x.PerformanceDate.Year == year && x.PerformanceDate.Month == month).Select(x => new PerformanceDTO()
                {
                    EmployeeID = x.EmployeeID,
                    EmployeeName = x.Employee.Name,
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).ToListAsync();
            }
        }
        public async Task<List<PerformanceDTO>> PerformanceReportsForEmployeeInSpecificMonth(int id, int year, int month)
        {
            if (month == 0)
            {
                return await _context.Performances.Where(x => x.EmployeeID == id && x.PerformanceDate.Year == year).Select(x => new PerformanceDTO()
                {
                    EmployeeID = x.EmployeeID,
                    EmployeeName = x.Employee.Name,
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).ToListAsync();
            }
            else if (month > 12 || month < 0)
            {
                throw new Exception("Wrong input, only months between 1-12 are accepted");
            }
            else
            {
                return await _context.Performances.Where(x => x.Employee.ID == id && x.PerformanceDate.Year == year && x.PerformanceDate.Month == month).Select(x => new PerformanceDTO()
                {
                    EmployeeID = x.EmployeeID,
                    EmployeeName = x.Employee.Name,
                    PerformanceDate = x.PerformanceDate,
                    Commitment = x.Commitment,
                    Communication = x.Communication,
                    Efficiency = x.Efficiency,
                    QualityOfWork = x.QualityOfWork,
                    TimeManagement = x.TimeManagement,
                    Overall = x.Overall
                }).ToListAsync();
            }
        }


        public async Task UpdatePerformance(int id, UpdatePerformanceDTO performance)
        {
            var oldPerformance = await _context.Performances.FindAsync(id);

            if (oldPerformance != null)
            {
                oldPerformance.Commitment = performance.Commitment;
                oldPerformance.Efficiency = performance.Efficiency;
                oldPerformance.QualityOfWork = performance.QualityOfWork;
                oldPerformance.TimeManagement = performance.TimeManagement;
                oldPerformance.Communication = performance.Communication;

                _context.Entry(oldPerformance).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Performance was not found");
            }
        }

        public async Task DeletePerformance(int id)
        {
            Performance performance = await _context.Performances.FindAsync(id);

            if (performance == null)
            {
                throw new Exception("Report was not found");
            }
            _context.Entry(performance).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}