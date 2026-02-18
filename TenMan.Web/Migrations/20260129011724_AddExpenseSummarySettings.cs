using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class AddExpenseSummarySettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseSummarySettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommitteeId = table.Column<int>(nullable: false),
                    HeaderAlignment = table.Column<int>(nullable: false),
                    ShowUnitDetails = table.Column<bool>(nullable: false),
                    ShowPercentages = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseSummarySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseSummarySettings_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseSummarySettings_CommitteeId",
                table: "ExpenseSummarySettings",
                column: "CommitteeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseSummarySettings");
        }
    }
}
