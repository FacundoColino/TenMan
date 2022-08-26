using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenMan.Web.Migrations
{
    public partial class UpdateDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Committee_Administrator_AdministratorId",
                table: "Committee");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Receipt_ReceiptId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Tenants_TenantId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Specialty_SpecialityId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Tenants_TenantId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Worker_WorkerId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestImage_Request_RequestId",
                table: "RequestImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Request_RequestId",
                table: "Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Committee_CommitteeId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Tenants_TenantId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Specialty_SpecialtyId",
                table: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Worker",
                table: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialty",
                table: "Specialty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestImage",
                table: "RequestImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receipt",
                table: "Receipt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Committee",
                table: "Committee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Worker",
                newName: "Workers");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Specialty",
                newName: "Specialties");

            migrationBuilder.RenameTable(
                name: "RequestImage",
                newName: "RequestImages");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "Receipt",
                newName: "Receipts");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Committee",
                newName: "Committees");

            migrationBuilder.RenameTable(
                name: "Administrator",
                newName: "Administrators");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_SpecialtyId",
                table: "Workers",
                newName: "IX_Workers_SpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_TenantId",
                table: "Units",
                newName: "IX_Units_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_CommitteeId",
                table: "Units",
                newName: "IX_Units_CommitteeId");

            migrationBuilder.RenameIndex(
                name: "IX_Status_RequestId",
                table: "Statuses",
                newName: "IX_Statuses_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestImage_RequestId",
                table: "RequestImages",
                newName: "IX_RequestImages_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_WorkerId",
                table: "Requests",
                newName: "IX_Requests_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_TenantId",
                table: "Requests",
                newName: "IX_Requests_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_SpecialityId",
                table: "Requests",
                newName: "IX_Requests_SpecialityId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_TenantId",
                table: "Payments",
                newName: "IX_Payments_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_ReceiptId",
                table: "Payments",
                newName: "IX_Payments_ReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_Committee_AdministratorId",
                table: "Committees",
                newName: "IX_Committees_AdministratorId");

            migrationBuilder.AddColumn<int>(
                name: "StatusTypeId",
                table: "Statuses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers",
                table: "Workers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestImages",
                table: "RequestImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receipts",
                table: "Receipts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Committees",
                table: "Committees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StatusType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StatusTypeId",
                table: "Statuses",
                column: "StatusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Committees_Administrators_AdministratorId",
                table: "Committees",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Receipts_ReceiptId",
                table: "Payments",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Tenants_TenantId",
                table: "Payments",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestImages_Requests_RequestId",
                table: "RequestImages",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Specialties_SpecialityId",
                table: "Requests",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Tenants_TenantId",
                table: "Requests",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Workers_WorkerId",
                table: "Requests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Requests_RequestId",
                table: "Statuses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_StatusType_StatusTypeId",
                table: "Statuses",
                column: "StatusTypeId",
                principalTable: "StatusType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Committees_CommitteeId",
                table: "Units",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Tenants_TenantId",
                table: "Units",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Specialties_SpecialtyId",
                table: "Workers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Committees_Administrators_AdministratorId",
                table: "Committees");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Receipts_ReceiptId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Tenants_TenantId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestImages_Requests_RequestId",
                table: "RequestImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Specialties_SpecialityId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Tenants_TenantId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Workers_WorkerId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Requests_RequestId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusType_StatusTypeId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Committees_CommitteeId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Tenants_TenantId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Specialties_SpecialtyId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "StatusType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers",
                table: "Workers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_StatusTypeId",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestImages",
                table: "RequestImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receipts",
                table: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Committees",
                table: "Committees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "StatusTypeId",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "Worker");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Specialties",
                newName: "Specialty");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameTable(
                name: "RequestImages",
                newName: "RequestImage");

            migrationBuilder.RenameTable(
                name: "Receipts",
                newName: "Receipt");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "Committees",
                newName: "Committee");

            migrationBuilder.RenameTable(
                name: "Administrators",
                newName: "Administrator");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_SpecialtyId",
                table: "Worker",
                newName: "IX_Worker_SpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_TenantId",
                table: "Unit",
                newName: "IX_Unit_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_CommitteeId",
                table: "Unit",
                newName: "IX_Unit_CommitteeId");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_RequestId",
                table: "Status",
                newName: "IX_Status_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_WorkerId",
                table: "Request",
                newName: "IX_Request_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_TenantId",
                table: "Request",
                newName: "IX_Request_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_SpecialityId",
                table: "Request",
                newName: "IX_Request_SpecialityId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestImages_RequestId",
                table: "RequestImage",
                newName: "IX_RequestImage_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_TenantId",
                table: "Payment",
                newName: "IX_Payment_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReceiptId",
                table: "Payment",
                newName: "IX_Payment_ReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_Committees_AdministratorId",
                table: "Committee",
                newName: "IX_Committee_AdministratorId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Status",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Worker",
                table: "Worker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialty",
                table: "Specialty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestImage",
                table: "RequestImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receipt",
                table: "Receipt",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Committee",
                table: "Committee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Committee_Administrator_AdministratorId",
                table: "Committee",
                column: "AdministratorId",
                principalTable: "Administrator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Receipt_ReceiptId",
                table: "Payment",
                column: "ReceiptId",
                principalTable: "Receipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Tenants_TenantId",
                table: "Payment",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Specialty_SpecialityId",
                table: "Request",
                column: "SpecialityId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Tenants_TenantId",
                table: "Request",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Worker_WorkerId",
                table: "Request",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestImage_Request_RequestId",
                table: "RequestImage",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Request_RequestId",
                table: "Status",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Committee_CommitteeId",
                table: "Unit",
                column: "CommitteeId",
                principalTable: "Committee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Tenants_TenantId",
                table: "Unit",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Specialty_SpecialtyId",
                table: "Worker",
                column: "SpecialtyId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
