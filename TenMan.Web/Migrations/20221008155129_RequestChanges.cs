using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class RequestChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UnitId",
                table: "Requests",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Units_UnitId",
                table: "Requests",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Units_UnitId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UnitId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Requests");
        }
    }
}
