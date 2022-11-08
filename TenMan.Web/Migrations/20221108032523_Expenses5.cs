using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class Expenses5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Expenses_ExpensesId",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ExpensesId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "Costs");

            migrationBuilder.CreateTable(
                name: "ExpensesCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    FieldId = table.Column<int>(nullable: false),
                    ExpensesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesCost_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesCost_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesCost_ExpensesId",
                table: "ExpensesCost",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesCost_FieldId",
                table: "ExpensesCost",
                column: "FieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesCost");

            migrationBuilder.AddColumn<int>(
                name: "ExpensesId",
                table: "Costs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ExpensesId",
                table: "Costs",
                column: "ExpensesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Expenses_ExpensesId",
                table: "Costs",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
