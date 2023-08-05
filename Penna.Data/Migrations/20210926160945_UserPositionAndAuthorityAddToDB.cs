using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class UserPositionAndAuthorityAddToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectPositionId",
                table: "BlockTeams");

            migrationBuilder.AddColumn<int>(
                name: "UserPositionId",
                table: "BlockTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Authorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorityGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    AuthorityGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPositions_AuthorityGroup_AuthorityGroupId",
                        column: x => x.AuthorityGroupId,
                        principalTable: "AuthorityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPositionAuthorities",
                columns: table => new
                {
                    UserPositionId = table.Column<int>(maxLength: 128, nullable: false),
                    AuthorityId = table.Column<int>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPositionAuthorities", x => new { x.UserPositionId, x.AuthorityId });
                    table.ForeignKey(
                        name: "FK_UserPositionAuthorities_Authority_AuthorityId",
                        column: x => x.AuthorityId,
                        principalTable: "Authorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPositionAuthorities_UserPosition_UserPositionId",
                        column: x => x.UserPositionId,
                        principalTable: "UserPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockTeams_UserId",
                table: "BlockTeams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockTeams_UserPositionId",
                table: "BlockTeams",
                column: "UserPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorityId",
                table: "UserPositionAuthorities",
                column: "AuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPositionId",
                table: "UserPositionAuthorities",
                column: "UserPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPositions_AuthorityGroupId",
                table: "UserPositions",
                column: "AuthorityGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockTeams_User_UserId",
                table: "BlockTeams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockTeams_UserPosition_UserPositionId",
                table: "BlockTeams",
                column: "UserPositionId",
                principalTable: "UserPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockTeams_User_UserId",
                table: "BlockTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockTeams_UserPosition_UserPositionId",
                table: "BlockTeams");

            migrationBuilder.DropTable(
                name: "UserPositionAuthorities");

            migrationBuilder.DropTable(
                name: "Authorities");

            migrationBuilder.DropTable(
                name: "UserPositions");

            migrationBuilder.DropTable(
                name: "AuthorityGroups");

            migrationBuilder.DropIndex(
                name: "IX_BlockTeams_UserId",
                table: "BlockTeams");

            migrationBuilder.DropIndex(
                name: "IX_BlockTeams_UserPositionId",
                table: "BlockTeams");

            migrationBuilder.DropColumn(
                name: "UserPositionId",
                table: "BlockTeams");

            migrationBuilder.AddColumn<int>(
                name: "ProjectPositionId",
                table: "BlockTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
