using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class UpdateProjectRemoveInstallment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccountDetails_Installment_InstallmentId",
                table: "CurrentAccountDetails");

            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAccountDetails_InstallmentId",
                table: "CurrentAccountDetails");

            migrationBuilder.DropColumn(
                name: "InstallmentId",
                table: "CurrentAccountDetails");

            migrationBuilder.AddColumn<double>(
                name: "ComitmentAmount",
                table: "Projects",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownPaymentAmount",
                table: "Projects",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DownPaymentDate",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "DownPaymentRate",
                table: "Projects",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentCount",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComitmentAmount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DownPaymentAmount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DownPaymentDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DownPaymentRate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "InstallmentCount",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentId",
                table: "CurrentAccountDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InstallmentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InstallmentNo = table.Column<byte>(type: "tinyint", nullable: false),
                    Payment = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountDetails_InstallmentId",
                table: "CurrentAccountDetails",
                column: "InstallmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_ProjectId",
                table: "Installments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccountDetails_Installment_InstallmentId",
                table: "CurrentAccountDetails",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
