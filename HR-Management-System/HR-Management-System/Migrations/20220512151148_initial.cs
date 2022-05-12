using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Management_System.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Present = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSalary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalarySlips",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySlips", x => new { x.EmployeeID, x.Date });
                    table.ForeignKey(
                        name: "FK_SalarySlips_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendances",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    SalarySlipDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalarySlipEmployeeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendances", x => new { x.AttendanceID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_Attendances_AttendanceID",
                        column: x => x.AttendanceID,
                        principalTable: "Attendances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_SalarySlips_SalarySlipEmployeeID_SalarySlipDate",
                        columns: x => new { x.SalarySlipEmployeeID, x.SalarySlipDate },
                        principalTable: "SalarySlips",
                        principalColumns: new[] { "EmployeeID", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approval = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    SalarySlipDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalarySlipEmployeeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tickets_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_SalarySlips_SalarySlipEmployeeID_SalarySlipDate",
                        columns: x => new { x.SalarySlipEmployeeID, x.SalarySlipDate },
                        principalTable: "SalarySlips",
                        principalColumns: new[] { "EmployeeID", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "ID", "Date", "EmployeeID", "Present" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true },
                    { 2, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false },
                    { 3, new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true },
                    { 4, new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false },
                    { 5, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true },
                    { 6, new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "BaseSalary", "Name" },
                values: new object[,]
                {
                    { 1, 500.0, "IT" },
                    { 2, 400.0, "Finance" },
                    { 3, 300.0, "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "ID", "Approval", "Comment", "Date", "EmployeeID", "SalarySlipDate", "SalarySlipEmployeeID", "Type", "emp_id" },
                values: new object[,]
                {
                    { 1, true, "Vacation", new DateTime(2022, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, -10, 0 },
                    { 2, false, "Car Loan", new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1000, 0 },
                    { 3, true, "Need more money", new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 20, 0 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "Level", "Name", "Password", "Phone" },
                values: new object[] { 1, 21, 1, "Employee1@LTUC.com", "Male", 1, "Laith", "1234", "079" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "Level", "Name", "Password", "Phone" },
                values: new object[] { 2, 22, 2, "Employee2@LTUC.com", "Other", 2, "Osama", "1234", "079" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "Level", "Name", "Password", "Phone" },
                values: new object[] { 3, 24, 3, "Employee3@LTUC.com", "Male", 3, "Shadi", "1234", "079" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_EmployeeID",
                table: "EmployeeAttendances",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_SalarySlipEmployeeID_SalarySlipDate",
                table: "EmployeeAttendances",
                columns: new[] { "SalarySlipEmployeeID", "SalarySlipDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EmployeeID",
                table: "Tickets",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SalarySlipEmployeeID_SalarySlipDate",
                table: "Tickets",
                columns: new[] { "SalarySlipEmployeeID", "SalarySlipDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAttendances");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "SalarySlips");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
