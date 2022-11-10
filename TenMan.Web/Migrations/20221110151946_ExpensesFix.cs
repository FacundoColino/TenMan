using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class ExpensesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExpA",
                table: "UnitDescriptionLines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingBalance",
                table: "UnitDescriptionLines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousBalance",
                table: "UnitDescriptionLines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "YourPayment",
                table: "UnitDescriptionLines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExpABalance",
                table: "CheckingAccounts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingBalance",
                table: "CheckingAccounts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpA",
                table: "UnitDescriptionLines");

            migrationBuilder.DropColumn(
                name: "PendingBalance",
                table: "UnitDescriptionLines");

            migrationBuilder.DropColumn(
                name: "PreviousBalance",
                table: "UnitDescriptionLines");

            migrationBuilder.DropColumn(
                name: "YourPayment",
                table: "UnitDescriptionLines");

            migrationBuilder.DropColumn(
                name: "ExpABalance",
                table: "CheckingAccounts");

            migrationBuilder.DropColumn(
                name: "PendingBalance",
                table: "CheckingAccounts");
        }
    }
}
