using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class UpdateCommitmentDateNameFromProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComitmentAmount",
                table: "Projects");

            migrationBuilder.AddColumn<double>(
                name: "CommitmentAmount",
                table: "Projects",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommitmentAmount",
                table: "Projects");

            migrationBuilder.AddColumn<double>(
                name: "ComitmentAmount",
                table: "Projects",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
