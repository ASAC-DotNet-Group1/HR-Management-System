using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Management_System.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    LeaveCredit = table.Column<int>(type: "int", nullable: false),
                    VacationCredit = table.Column<int>(type: "int", nullable: false)
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
                name: "Performances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Commitment = table.Column<int>(type: "int", nullable: false),
                    Efficiency = table.Column<int>(type: "int", nullable: false),
                    Communication = table.Column<int>(type: "int", nullable: false),
                    TimeManagement = table.Column<int>(type: "int", nullable: false),
                    QualityOfWork = table.Column<int>(type: "int", nullable: false),
                    Overall = table.Column<double>(type: "float", nullable: false),
                    PerformanceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Performances_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
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
                name: "Attendances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    StartShift = table.Column<bool>(type: "bit", nullable: false),
                    EndShift = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalarySlipDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalarySlipEmployeeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_SalarySlips_SalarySlipEmployeeID_SalarySlipDate",
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
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_SalarySlips_SalarySlipEmployeeID_SalarySlipDate",
                        columns: x => new { x.SalarySlipEmployeeID, x.SalarySlipDate },
                        principalTable: "SalarySlips",
                        principalColumns: new[] { "EmployeeID", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "IT" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Finance" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Sales" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "LeaveCredit", "Level", "Name", "Password", "Phone", "Salary", "VacationCredit" },
                values: new object[] { 1, 21, 1, "Employee1@LTUC.com", "Male", 14, 1, "Laith", "1234", "079", 300.0, 14 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "LeaveCredit", "Level", "Name", "Password", "Phone", "Salary", "VacationCredit" },
                values: new object[] { 2, 22, 2, "Employee2@LTUC.com", "Other", 14, 2, "Osama", "1234", "079", 400.0, 14 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "LeaveCredit", "Level", "Name", "Password", "Phone", "Salary", "VacationCredit" },
                values: new object[] { 3, 24, 3, "Employee3@LTUC.com", "Male", 14, 3, "Shadi", "1234", "079", 500.0, 14 });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "ID", "EmpName", "EmployeeID", "EndDate", "EndShift", "SalarySlipDate", "SalarySlipEmployeeID", "StartDate", "StartShift" },
                values: new object[,]
                {
                    { 1, "Shadi Aslan", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, "Osama Alzaghal", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 3, "Ahmad Masadeh", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 4, "Laith Alamat", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 5, "Johnson Johnson", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 6, "Johnny Adam", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "ID", "Commitment", "Communication", "Efficiency", "EmployeeID", "Overall", "PerformanceDate", "QualityOfWork", "TimeManagement" },
                values: new object[] { 1, 0, 0, 0, 2, 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "ID", "Amount", "Comment", "Date", "EmployeeID", "SalarySlipDate", "SalarySlipEmployeeID", "Status", "Total", "Type" },
                values: new object[,]
                {
                    { 4, 2, "Need more and more money", new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, 1, 400.0, 2 },
                    { 1, 2, "Vacation", new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, 1, -40.0, 0 },
                    { 2, 2, "Car Loan", new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, 2, 0.0, 1 },
                    { 3, 2, "Need more money", new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, 1, 400.0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeID",
                table: "Attendances",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_SalarySlipEmployeeID_SalarySlipDate",
                table: "Attendances",
                columns: new[] { "SalarySlipEmployeeID", "SalarySlipDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_EmployeeID",
                table: "Performances",
                column: "EmployeeID");

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
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "SalarySlips");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
