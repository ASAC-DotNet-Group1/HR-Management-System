﻿// <auto-generated />
using System;
using HR_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HR_Management_System.Migrations
{
    [DbContext(typeof(HR_DbContext))]
    partial class HR_DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HR_Management_System.Models.Attendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<bool>("Present")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("SalarySlipDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SalarySlipEmployeeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("SalarySlipEmployeeID", "SalarySlipDate");

                    b.ToTable("Attendances");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Date = new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeID = 1,
                            Present = true
                        },
                        new
                        {
                            ID = 2,
                            Date = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeID = 1,
                            Present = false
                        },
                        new
                        {
                            ID = 3,
                            Date = new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeID = 1,
                            Present = true
                        },
                        new
                        {
                            ID = 4,
                            Date = new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeID = 2,
                            Present = false
                        },
                        new
                        {
                            ID = 5,
                            Date = new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeID = 2,
                            Present = true
                        },
                        new
                        {
                            ID = 6,
                            Date = new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeID = 2,
                            Present = true
                        });
                });

            modelBuilder.Entity("HR_Management_System.Models.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "IT"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Finance"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Sales"
                        });
                });

            modelBuilder.Entity("HR_Management_System.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Age = 21,
                            DepartmentID = 1,
                            Email = "Employee1@LTUC.com",
                            Gender = "Male",
                            Level = 1,
                            Name = "Laith",
                            Password = "1234",
                            Phone = "079",
                            Salary = 300.0
                        },
                        new
                        {
                            ID = 2,
                            Age = 22,
                            DepartmentID = 2,
                            Email = "Employee2@LTUC.com",
                            Gender = "Other",
                            Level = 2,
                            Name = "Osama",
                            Password = "1234",
                            Phone = "079",
                            Salary = 400.0
                        },
                        new
                        {
                            ID = 3,
                            Age = 24,
                            DepartmentID = 3,
                            Email = "Employee3@LTUC.com",
                            Gender = "Male",
                            Level = 3,
                            Name = "Shadi",
                            Password = "1234",
                            Phone = "079",
                            Salary = 500.0
                        });
                });

            modelBuilder.Entity("HR_Management_System.Models.SalarySlip", b =>
                {
                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("EmployeeID", "Date");

                    b.ToTable("SalarySlips");
                });

            modelBuilder.Entity("HR_Management_System.Models.Ticket", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approval")
                        .HasColumnType("bit");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SalarySlipDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SalarySlipEmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("emp_id")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("SalarySlipEmployeeID", "SalarySlipDate");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Approval = true,
                            Comment = "Vacation",
                            Date = new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = -10,
                            emp_id = 2
                        },
                        new
                        {
                            ID = 2,
                            Approval = false,
                            Comment = "Car Loan",
                            Date = new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1000,
                            emp_id = 2
                        },
                        new
                        {
                            ID = 3,
                            Approval = true,
                            Comment = "Need more money",
                            Date = new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 20,
                            emp_id = 2
                        });
                });

            modelBuilder.Entity("HR_Management_System.Models.Attendance", b =>
                {
                    b.HasOne("HR_Management_System.Models.Employee", "Employee")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HR_Management_System.Models.SalarySlip", null)
                        .WithMany("Attendances")
                        .HasForeignKey("SalarySlipEmployeeID", "SalarySlipDate");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HR_Management_System.Models.Employee", b =>
                {
                    b.HasOne("HR_Management_System.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("HR_Management_System.Models.SalarySlip", b =>
                {
                    b.HasOne("HR_Management_System.Models.Employee", "Employee")
                        .WithMany("SalarySlip")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HR_Management_System.Models.Ticket", b =>
                {
                    b.HasOne("HR_Management_System.Models.Employee", "Employee")
                        .WithMany("Ticket")
                        .HasForeignKey("EmployeeID");

                    b.HasOne("HR_Management_System.Models.SalarySlip", null)
                        .WithMany("Ticket")
                        .HasForeignKey("SalarySlipEmployeeID", "SalarySlipDate");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HR_Management_System.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HR_Management_System.Models.Employee", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("SalarySlip");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("HR_Management_System.Models.SalarySlip", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Ticket");
                });
#pragma warning restore 612, 618
        }
    }
}
