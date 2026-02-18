using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class FixReceiptImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptImages_Receipts_ReceiptId",
                table: "ReceiptImages");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptImages_ReceiptId",
                table: "ReceiptImages");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "ReceiptImages",
                newName: "ExpenseCostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseCostId",
                table: "ReceiptImages",
                newName: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptImages_ReceiptId",
                table: "ReceiptImages",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptImages_Receipts_ReceiptId",
                table: "ReceiptImages",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
