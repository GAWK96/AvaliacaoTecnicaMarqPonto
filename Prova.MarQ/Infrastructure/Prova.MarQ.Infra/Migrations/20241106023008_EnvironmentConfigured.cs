using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prova.MarQ.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EnvironmentConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyDocument = table.Column<string>(type: "TEXT", nullable: true),
                    IsCompanyDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeePin = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeName = table.Column<string>(type: "TEXT", nullable: true),
                    EmployeeDocument = table.Column<string>(type: "TEXT", nullable: true),
                    IsEmployeeDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeePin);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clocking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeePin = table.Column<string>(type: "TEXT", nullable: true),
                    ClockIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClockOut = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clocking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clocking_Employees_EmployeePin",
                        column: x => x.EmployeePin,
                        principalTable: "Employees",
                        principalColumn: "EmployeePin");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clocking_EmployeePin",
                table: "Clocking",
                column: "EmployeePin");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyDocument",
                table: "Companies",
                column: "CompanyDocument",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeDocument",
                table: "Employees",
                column: "EmployeeDocument",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeePin",
                table: "Employees",
                column: "EmployeePin",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clocking");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
