using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class AddBlockSubContBusinessGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessGroupId",
                table: "BlockSubcontractors");

            migrationBuilder.CreateTable(
                name: "BlockSubcontractorBusinessGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockSubcontractorId = table.Column<int>(nullable: false),
                    BusinessGroupId = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockSubcontractorBusinessGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockSubcontractorBusinessGroups_BlockSubcontractor_BlockSubcontractorId",
                        column: x => x.BlockSubcontractorId,
                        principalTable: "BlockSubcontractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockSubcontractorBusinessGroups_BlockSubcontractorId",
                table: "BlockSubcontractorBusinessGroups",
                column: "BlockSubcontractorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockSubcontractorBusinessGroups");

            migrationBuilder.AddColumn<byte>(
                name: "BusinessGroupId",
                table: "BlockSubcontractors",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
