using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class AddOwnerToUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Tenants_TenantId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_TenantId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Units",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TenantId",
                table: "Requests",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Tenants_TenantId",
                table: "Requests",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
