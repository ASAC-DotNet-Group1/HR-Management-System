using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Management_System.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalarySlips",
                table: "SalarySlips");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SalarySlips");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalarySlips",
                table: "SalarySlips",
                columns: new[] { "EmployeeID", "Date" });

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
                    { 1, 500m, "IT" },
                    { 2, 400m, "Finance" },
                    { 3, 300m, "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Age", "DepartmentID", "Email", "Gender", "Level", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, 21, 1, "Employee1@LTUC.com", "Male", 1, "Laith", "1234", "079" },
                    { 2, 22, 2, "Employee2@LTUC.com", "Other", 2, "Osama", "1234", "079" },
                    { 3, 24, 3, "Employee3@LTUC.com", "Male", 3, "Shadi", "1234", "079" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "ID", "Approval", "Comment", "Date", "Type" },
                values: new object[,]
                {
                    { 1, true, "Vacation", new DateTime(2022, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), -10 },
                    { 2, false, "Car Loan", new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000 },
                    { 3, true, "Need more money", new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalarySlips",
                table: "SalarySlips");

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "SalarySlips",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalarySlips",
                table: "SalarySlips",
                column: "ID");
        }
    }
}
