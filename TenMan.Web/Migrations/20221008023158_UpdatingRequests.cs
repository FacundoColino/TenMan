using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class UpdatingRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Statuses_StatusId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_StatusId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Statuses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_RequestId",
                table: "Statuses",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Requests_RequestId",
                table: "Statuses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Requests_RequestId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_RequestId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StatusId",
                table: "Requests",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Statuses_StatusId",
                table: "Requests",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
