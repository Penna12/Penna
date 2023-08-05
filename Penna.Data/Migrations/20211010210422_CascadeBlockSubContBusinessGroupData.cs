using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class CascadeBlockSubContBusinessGroupData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockSubcontractorBusinessGroups_BlockSubcontractor_BlockSubcontractorId",
                table: "BlockSubcontractorBusinessGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockSubcontractorBusinessGroups_BlockSubcontractor_BlockSubcontractorId",
                table: "BlockSubcontractorBusinessGroups",
                column: "BlockSubcontractorId",
                principalTable: "BlockSubcontractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockSubcontractorBusinessGroups_BlockSubcontractor_BlockSubcontractorId",
                table: "BlockSubcontractorBusinessGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockSubcontractorBusinessGroups_BlockSubcontractor_BlockSubcontractorId",
                table: "BlockSubcontractorBusinessGroups",
                column: "BlockSubcontractorId",
                principalTable: "BlockSubcontractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
