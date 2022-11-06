using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class NuevasExpensas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "Units",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CUIT",
                table: "Committees",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SuterhKey",
                table: "Committees",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CUIT",
                table: "Administrators",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CUIT",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "SuterhKey",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "CUIT",
                table: "Administrators");
        }
    }
}
