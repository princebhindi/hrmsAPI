using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Register.PERSISTANCE.Migrations
{
    /// <inheritdoc />
    public partial class AddJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OnCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OnUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_DepartMents_DeptId",
                        column: x => x.DeptId,
                        principalTable: "DepartMents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DeptId",
                table: "Jobs",
                column: "DeptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
