using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class NewExpenses3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitDescriptionLine_Expenses_ExpensesId",
                table: "UnitDescriptionLine");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitDescriptionLine_Units_UnitId",
                table: "UnitDescriptionLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitDescriptionLine",
                table: "UnitDescriptionLine");

            migrationBuilder.RenameTable(
                name: "UnitDescriptionLine",
                newName: "UnitDescriptionLines");

            migrationBuilder.RenameIndex(
                name: "IX_UnitDescriptionLine_UnitId",
                table: "UnitDescriptionLines",
                newName: "IX_UnitDescriptionLines_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitDescriptionLine_ExpensesId",
                table: "UnitDescriptionLines",
                newName: "IX_UnitDescriptionLines_ExpensesId");

            migrationBuilder.AlterColumn<int>(
                name: "ExpensesId",
                table: "UnitDescriptionLines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitDescriptionLines",
                table: "UnitDescriptionLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDescriptionLines_Expenses_ExpensesId",
                table: "UnitDescriptionLines",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDescriptionLines_Units_UnitId",
                table: "UnitDescriptionLines",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitDescriptionLines_Expenses_ExpensesId",
                table: "UnitDescriptionLines");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitDescriptionLines_Units_UnitId",
                table: "UnitDescriptionLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitDescriptionLines",
                table: "UnitDescriptionLines");

            migrationBuilder.RenameTable(
                name: "UnitDescriptionLines",
                newName: "UnitDescriptionLine");

            migrationBuilder.RenameIndex(
                name: "IX_UnitDescriptionLines_UnitId",
                table: "UnitDescriptionLine",
                newName: "IX_UnitDescriptionLine_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitDescriptionLines_ExpensesId",
                table: "UnitDescriptionLine",
                newName: "IX_UnitDescriptionLine_ExpensesId");

            migrationBuilder.AlterColumn<int>(
                name: "ExpensesId",
                table: "UnitDescriptionLine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitDescriptionLine",
                table: "UnitDescriptionLine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDescriptionLine_Expenses_ExpensesId",
                table: "UnitDescriptionLine",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDescriptionLine_Units_UnitId",
                table: "UnitDescriptionLine",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
