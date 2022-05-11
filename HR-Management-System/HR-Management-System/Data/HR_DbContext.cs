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
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public DbSet<SalarySlip> SalarySlips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public HR_DbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { ID = 1 , Name = "Laith", Age = 21, Gender = "Male", DepartmentID = 1, Level = Level.Junior, Email = "Employee1@LTUC.com",    Password="1234", Phone="079" },
                new Employee { ID = 2, Name = "Osama", Age = 22, Gender = "Other", DepartmentID = 2, Level = Level.MidSenior, Email = "Employee2@LTUC.com", Password = "1234", Phone = "079" },
                new Employee { ID = 3, Name = "Shadi", Age = 24, Gender = "Male", DepartmentID = 3, Level = Level.Senior, Email = "Employee3@LTUC.com", Password = "1234", Phone = "079" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { ID = 1, Name = "IT", BaseSalary= 500 },
                new Department { ID = 2, Name = "Finance", BaseSalary = 400 },
                new Department { ID = 3, Name = "Sales", BaseSalary = 300 }
                );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { ID = 1, Approval = true, Comment = "Vacation", Date = new DateTime(2022, 5, 22), Type = Models.Type.Leave },
                new Ticket { ID = 2, Approval = false, Comment = "Car Loan", Date = new DateTime(2022, 5, 23), Type = Models.Type.Loan },
                new Ticket { ID = 3, Approval = true, Comment = "Need more money", Date = new DateTime(2022, 5, 23), Type = Models.Type.Overtime }
                );

            modelBuilder.Entity<Attendance>().HasData(
                new Attendance { ID = 1, EmployeeID=1, Present=true, Date = new DateTime(2022, 6, 23)},
                new Attendance { ID = 2, EmployeeID = 1, Present = false, Date = new DateTime(2022, 6, 24) },
                new Attendance { ID = 3, EmployeeID = 1, Present = true, Date = new DateTime(2022, 6, 25) },
                new Attendance { ID = 4, EmployeeID = 2, Present = false, Date = new DateTime(2022, 6, 23) },
                new Attendance { ID = 5, EmployeeID = 2, Present = true, Date = new DateTime(2022, 6, 24) },
                new Attendance { ID = 6, EmployeeID = 2, Present = true, Date = new DateTime(2022, 6, 25) }
                );


            // Keys added for join tables.
            modelBuilder.Entity<EmployeeAttendance>().HasKey(x => new { x.AttendanceID, x.EmployeeID });
            modelBuilder.Entity<SalarySlip>().HasKey(x => new { x.EmployeeID, x.Date });

        }
    }
}
