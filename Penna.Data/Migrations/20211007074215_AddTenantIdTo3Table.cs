using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class AddTenantIdTo3Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "UserPositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AuthorityGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Authorities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserPositions_TenantId",
                table: "UserPositions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorityGroups_TenantId",
                table: "AuthorityGroups",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorities_TenantId",
                table: "Authorities",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authority_Tenants_TenantId",
                table: "Authorities",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorityGroup_Tenants_TenantId",
                table: "AuthorityGroups",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosition_Tenants_TenantId",
                table: "UserPositions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authority_Tenants_TenantId",
                table: "Authorities");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorityGroup_Tenants_TenantId",
                table: "AuthorityGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPosition_Tenants_TenantId",
                table: "UserPositions");

            migrationBuilder.DropIndex(
                name: "IX_UserPositions_TenantId",
                table: "UserPositions");

            migrationBuilder.DropIndex(
                name: "IX_AuthorityGroups_TenantId",
                table: "AuthorityGroups");

            migrationBuilder.DropIndex(
                name: "IX_Authorities_TenantId",
                table: "Authorities");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "UserPositions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AuthorityGroups");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Authorities");
        }
    }
}
