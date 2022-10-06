using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class UnitsAndPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "CheckingAccounts",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UnitId",
                table: "Payments",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Units_UnitId",
                table: "Payments",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Units_UnitId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UnitId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "CheckingAccounts",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
