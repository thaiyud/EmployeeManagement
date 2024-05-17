using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class addfieldforform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Forms",
                newName: "DateSubmitted");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayEnd",
                table: "Forms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DayStart",
                table: "Forms",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayEnd",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "DayStart",
                table: "Forms");

            migrationBuilder.RenameColumn(
                name: "DateSubmitted",
                table: "Forms",
                newName: "Date");
        }
    }
}
