using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class NuevasExpensas3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommitteeId",
                table: "Costs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CommitteeId",
                table: "Costs",
                column: "CommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Committees_CommitteeId",
                table: "Costs",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Committees_CommitteeId",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_CommitteeId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "CommitteeId",
                table: "Costs");
        }
    }
}
