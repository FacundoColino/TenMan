using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class Expenses6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesCost_Expenses_ExpensesId",
                table: "ExpensesCost");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesCost_Field_FieldId",
                table: "ExpensesCost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpensesCost",
                table: "ExpensesCost");

            migrationBuilder.RenameTable(
                name: "ExpensesCost",
                newName: "ExpensesCosts");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesCost_FieldId",
                table: "ExpensesCosts",
                newName: "IX_ExpensesCosts_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesCost_ExpensesId",
                table: "ExpensesCosts",
                newName: "IX_ExpensesCosts_ExpensesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpensesCosts",
                table: "ExpensesCosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesCosts_Expenses_ExpensesId",
                table: "ExpensesCosts",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesCosts_Field_FieldId",
                table: "ExpensesCosts",
                column: "FieldId",
                principalTable: "Field",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesCosts_Expenses_ExpensesId",
                table: "ExpensesCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesCosts_Field_FieldId",
                table: "ExpensesCosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpensesCosts",
                table: "ExpensesCosts");

            migrationBuilder.RenameTable(
                name: "ExpensesCosts",
                newName: "ExpensesCost");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesCosts_FieldId",
                table: "ExpensesCost",
                newName: "IX_ExpensesCost_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesCosts_ExpensesId",
                table: "ExpensesCost",
                newName: "IX_ExpensesCost_ExpensesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpensesCost",
                table: "ExpensesCost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesCost_Expenses_ExpensesId",
                table: "ExpensesCost",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesCost_Field_FieldId",
                table: "ExpensesCost",
                column: "FieldId",
                principalTable: "Field",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
