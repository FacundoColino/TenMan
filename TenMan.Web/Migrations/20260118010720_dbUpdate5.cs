using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class dbUpdate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ExpensesCosts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ExpensesCosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesCosts_CategoryId",
                table: "ExpensesCosts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesCosts_Categories_CategoryId",
                table: "ExpensesCosts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesCosts_Categories_CategoryId",
                table: "ExpensesCosts");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesCosts_CategoryId",
                table: "ExpensesCosts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ExpensesCosts");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ExpensesCosts",
                nullable: false,
                defaultValue: "");
        }
    }
}
