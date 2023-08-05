using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class ChangeCountriesAddingFiels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryDialCode",
                table: "Tenants",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DialCode",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Countries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CountryDialCode",
                table: "AspNetUsers",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryDialCode",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DialCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryDialCode",
                table: "AspNetUsers");
        }
    }
}
