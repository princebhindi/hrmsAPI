using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Register.PERSISTANCE.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_Employees_EmployeeId",
                table: "EmployeeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Attendances");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmpId",
                table: "Salaries",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmpId",
                table: "Leaves",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmpId",
                table: "EmployeeDocuments",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmpId",
                table: "Attendances",
                column: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmpId",
                table: "Attendances",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_Employees_EmpId",
                table: "EmployeeDocuments",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmpId",
                table: "Leaves",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Employees_EmpId",
                table: "Salaries",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmpId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_Employees_EmpId",
                table: "EmployeeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmpId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Employees_EmpId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_EmpId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_EmpId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_EmpId",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EmpId",
                table: "Attendances");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Salaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Leaves",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "EmployeeDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Attendances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                table: "EmployeeDocuments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_Employees_EmployeeId",
                table: "EmployeeDocuments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                table: "Salaries",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
