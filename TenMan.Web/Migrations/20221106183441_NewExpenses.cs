using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class NewExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpensesId",
                table: "Field",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpensesId",
                table: "Costs",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "YourPayment",
                table: "CheckingAccounts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    CommitteeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitDescriptionLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<int>(nullable: true),
                    Interest = table.Column<decimal>(nullable: false),
                    NewUnitTotal = table.Column<decimal>(nullable: false),
                    ExpensesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitDescriptionLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitDescriptionLine_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitDescriptionLine_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_ExpensesId",
                table: "Field",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ExpensesId",
                table: "Costs",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CommitteeId",
                table: "Expenses",
                column: "CommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDescriptionLine_ExpensesId",
                table: "UnitDescriptionLine",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDescriptionLine_UnitId",
                table: "UnitDescriptionLine",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Expenses_ExpensesId",
                table: "Costs",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Expenses_ExpensesId",
                table: "Field",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Expenses_ExpensesId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Field_Expenses_ExpensesId",
                table: "Field");

            migrationBuilder.DropTable(
                name: "UnitDescriptionLine");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Field_ExpensesId",
                table: "Field");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ExpensesId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "YourPayment",
                table: "CheckingAccounts");
        }
    }
}
