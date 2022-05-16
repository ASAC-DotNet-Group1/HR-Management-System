using HR_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Data
{
    public class HR_DbContext : DbContext
    {
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalarySlip> SalarySlips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public HR_DbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { ID = 1, Name = "Laith", Age = 21, Gender = "Male", DepartmentID = 1, Salary = 300, Level = Level.Junior, Email = "Employee1@LTUC.com", Password = "1234", Phone = "079", LeaveCredit = 14, VacationCredit = 14 },
                new Employee { ID = 2, Name = "Osama", Age = 22, Gender = "Other", DepartmentID = 2, Salary = 400, Level = Level.MidSenior, Email = "Employee2@LTUC.com", Password = "1234", Phone = "079", LeaveCredit = 14, VacationCredit = 14 },
                new Employee { ID = 3, Name = "Shadi", Age = 24, Gender = "Male", DepartmentID = 3, Salary = 500, Level = Level.Senior, Email = "Employee3@LTUC.com", Password = "1234", Phone = "079", LeaveCredit = 14, VacationCredit = 14 }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { ID = 1, Name = "IT", },
                new Department { ID = 2, Name = "Finance", },
                new Department { ID = 3, Name = "Sales", }
                );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { ID = 1, EmployeeID = 2,  Status = Models.Status.Approved, Comment = "Vacation",Amount = 2, Total = -40, Date = new DateTime(2022, 5, 12), Type = Models.Type.Vacation },
                new Ticket { ID = 2, EmployeeID = 2,  Status = Models.Status.Denied, Comment = "Car Loan", Amount = 2, Total = 0 ,Date = new DateTime(2022, 5, 9), Type = Models.Type.Leave },
                new Ticket { ID = 3, EmployeeID = 2,  Status = Models.Status.Approved, Comment = "Need more money", Amount = 2, Total = 400, Date = new DateTime(2022, 5, 23), Type = Models.Type.Overtime },
                new Ticket { ID = 4, EmployeeID = 1,  Status = Models.Status.Approved, Comment = "Need more and more money", Amount = 2,Total = 400, Date = new DateTime(2022, 5, 23), Type = Models.Type.Overtime }
                );

            modelBuilder.Entity<Attendance>().HasData(
                new Attendance { ID = 1, EmployeeID = 1, EmpName = "Shadi Aslan", StartShift = true, StartDate = new DateTime(2022, 6, 23) },
                new Attendance { ID = 2, EmployeeID = 1, EmpName = "Osama Alzaghal", StartShift = false, StartDate = new DateTime(2022, 6, 24) },
                new Attendance { ID = 3, EmployeeID = 1, EmpName = "Ahmad Masadeh", StartShift = true, StartDate = new DateTime(2022, 6, 25) },
                new Attendance { ID = 4, EmployeeID = 2, EmpName = "Laith Alamat", StartShift = false, StartDate = new DateTime(2022, 5, 12) },
                new Attendance { ID = 5, EmployeeID = 2, EmpName = "Johnson Johnson", StartShift = true, StartDate = new DateTime(2022, 5, 10) },
                new Attendance { ID = 6, EmployeeID = 2, EmpName = "Johnny Adam", StartShift = true, StartDate = new DateTime(2022, 5, 25) }
                );

            modelBuilder.Entity<Performance>().HasData(
                new Performance { ID = 1, EmployeeID = 1, Commitment = 9, Communication = 9, Efficiency = 9, TimeManagement = 9, QualityOfWork = 9, Overall = 90, PerformanceDate = new DateTime(2022, 6, 23) },
                new Performance { ID = 2, EmployeeID = 1, Commitment = 9, Communication = 9, Efficiency = 9, TimeManagement = 9, QualityOfWork = 9, Overall = 90, PerformanceDate = new DateTime(2022, 6, 23) },
                new Performance { ID = 3, EmployeeID = 1, Commitment = 9, Communication = 9, Efficiency = 9, TimeManagement = 9, QualityOfWork = 9, Overall = 90, PerformanceDate = new DateTime(2022, 6, 23) },
                new Performance { ID = 4, EmployeeID = 2, Commitment = 9, Communication = 9, Efficiency = 9, TimeManagement = 9, QualityOfWork = 9, Overall = 90, PerformanceDate = new DateTime(2022, 6, 23) },
                new Performance { ID = 5, EmployeeID = 2, Commitment = 9, Communication = 9, Efficiency = 9, TimeManagement = 9, QualityOfWork = 9, Overall = 90, PerformanceDate = new DateTime(2022, 6, 23) },
                new Performance { ID = 6, EmployeeID = 2, Commitment = 9, Communication = 9, Efficiency = 9, TimeManagement = 9, QualityOfWork = 9, Overall = 90, PerformanceDate = new DateTime(2022, 6, 23) }
                );

            // Keys added for join tables.
            modelBuilder.Entity<SalarySlip>().HasKey(x => new { x.EmployeeID, x.Date });

        }
    }
}
