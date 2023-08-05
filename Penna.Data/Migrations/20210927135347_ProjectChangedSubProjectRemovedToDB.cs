using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class ProjectChangedSubProjectRemovedToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubProjects");

            migrationBuilder.DropColumn(
                name: "BuildingControl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Commercial",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployerCompanyName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployerEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployerFullName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployerPhone",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalHousing",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "ArchitecturalWorks",
                table: "Projects",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuildingInspection",
                table: "Projects",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentAccountId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ElectricalWorks",
                table: "Projects",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandScapeWorks",
                table: "Projects",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MechanicalWorks",
                table: "Projects",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaticWorks",
                table: "Projects",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalApartmentCount",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCommercialCount",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CurrentAccountId",
                table: "Projects",
                column: "CurrentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CurrentAccount_CurrentAccountId",
                table: "Projects",
                column: "CurrentAccountId",
                principalTable: "CurrentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CurrentAccount_CurrentAccountId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CurrentAccountId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ArchitecturalWorks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BuildingInspection",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CurrentAccountId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ElectricalWorks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LandScapeWorks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MechanicalWorks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StaticWorks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalApartmentCount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalCommercialCount",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "BuildingControl",
                table: "Projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Commercial",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployerCompanyName",
                table: "Projects",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployerEmail",
                table: "Projects",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployerFullName",
                table: "Projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployerPhone",
                table: "Projects",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalHousing",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubProjects",
                columns: table => new
                {
                    SubProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasementFlourCount = table.Column<byte>(type: "tinyint", nullable: false),
                    BlockCalculationType = table.Column<byte>(type: "tinyint", nullable: false),
                    CommonAreaCalculationType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    HouseCalculationType = table.Column<byte>(type: "tinyint", nullable: false),
                    IncludingGroundFlourCount = table.Column<byte>(type: "tinyint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsArchive = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProjectType = table.Column<byte>(type: "tinyint", nullable: false),
                    RemainingBlockCount = table.Column<byte>(type: "tinyint", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProjects", x => x.SubProjectId);
                    table.ForeignKey(
                        name: "FK_SubProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubProjects_ProjectId",
                table: "SubProjects",
                column: "ProjectId",
                unique: true);
        }
    }
}
