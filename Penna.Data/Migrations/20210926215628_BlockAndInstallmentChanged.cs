using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class BlockAndInstallmentChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntallmentNo",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "CommercialCount",
                table: "Blocks");

            migrationBuilder.AddColumn<byte>(
                name: "InstallmentNo",
                table: "Installments",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentNo",
                table: "Installments");

            migrationBuilder.AddColumn<byte>(
                name: "IntallmentNo",
                table: "Installments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "CommercialCount",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
