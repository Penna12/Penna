using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class RemoveDownPaymentFromProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownPaymentAmount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DownPaymentDate",
                table: "Projects");

            migrationBuilder.AlterColumn<byte>(
                name: "InstallmentCount",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InstallmentCount",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<double>(
                name: "DownPaymentAmount",
                table: "Projects",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DownPaymentDate",
                table: "Projects",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
