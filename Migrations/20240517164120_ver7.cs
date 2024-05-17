﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class ver7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonthlySalaries_AspNetUsers_UserId",
                table: "MonthlySalaries");

            migrationBuilder.DropIndex(
                name: "IX_MonthlySalaries_UserId",
                table: "MonthlySalaries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MonthlySalaries");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BasicSalaries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BasicSalaries_UserId1",
                table: "BasicSalaries",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicSalaries_AspNetUsers_UserId1",
                table: "BasicSalaries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicSalaries_AspNetUsers_UserId1",
                table: "BasicSalaries");

            migrationBuilder.DropIndex(
                name: "IX_BasicSalaries_UserId1",
                table: "BasicSalaries");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BasicSalaries");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MonthlySalaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySalaries_UserId",
                table: "MonthlySalaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlySalaries_AspNetUsers_UserId",
                table: "MonthlySalaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
