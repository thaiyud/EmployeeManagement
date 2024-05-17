using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class ver10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicSalaries_AspNetUsers_UserId",
                table: "BasicSalaries");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicSalaries_AspNetUsers_UserId",
                table: "BasicSalaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicSalaries_AspNetUsers_UserId",
                table: "BasicSalaries");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicSalaries_AspNetUsers_UserId",
                table: "BasicSalaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
