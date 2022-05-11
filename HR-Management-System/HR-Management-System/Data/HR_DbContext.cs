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

            //modelBuilder.Entity<Room>().HasData(
            //    new Room { ID = 1, Layout = "Grey and Red Hotel Room", Name = "Solace And Comfort" },
            //    new Room { ID = 2, Layout = "Greek Inspired Hotel Room", Name = "Stylish Greek" },
            //    new Room
            //    {
            //        ID = 3,
            //        Layout = "Blue Boutique Hotel Room Layout",
            //        Name = "Blue Room"
            //    });

            // Keys added for join tables.
            modelBuilder.Entity<EmployeeAttendance>().HasKey(x => new { x.AttendanceID, x.EmployeeID });
        }
    }
}
