using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class NewExpenses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Committees_CommitteeId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                table: "Expenses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Committees_CommitteeId",
                table: "Expenses",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Committees_CommitteeId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Committees_CommitteeId",
                table: "Expenses",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
