using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class CheckingAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckingAccountId",
                table: "Units",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckingAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    PreviousBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckingAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_CheckingAccountId",
                table: "Units",
                column: "CheckingAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_CheckingAccounts_CheckingAccountId",
                table: "Units",
                column: "CheckingAccountId",
                principalTable: "CheckingAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_CheckingAccounts_CheckingAccountId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "CheckingAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Units_CheckingAccountId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CheckingAccountId",
                table: "Units");
        }
    }
}
