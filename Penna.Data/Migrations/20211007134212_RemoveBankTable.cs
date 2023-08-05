using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class RemoveBankTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Bank_BankId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Bank_BankId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Banks_BankId",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_BankId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Subcontractors_BankId",
                table: "Subcontractors");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAccounts_BankId",
                table: "CurrentAccounts");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IBAN_Code",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Subcontractors");

            migrationBuilder.DropColumn(
                name: "IBAN_Code",
                table: "Subcontractors");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "CurrentAccounts");

            migrationBuilder.DropColumn(
                name: "IBAN_Code",
                table: "CurrentAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Vendors",
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Vendors",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Subcontractors",
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Subcontractors",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "CurrentAccounts",
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "CurrentAccounts",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Subcontractors");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "CurrentAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Vendors",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN_Code",
                table: "Vendors",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Subcontractors",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Subcontractors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN_Code",
                table: "Subcontractors",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "CurrentAccounts",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "CurrentAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN_Code",
                table: "CurrentAccounts",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BankId",
                table: "Vendors",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcontractors_BankId",
                table: "Subcontractors",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_BankId",
                table: "CurrentAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CountryId",
                table: "Banks",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Bank_BankId",
                table: "CurrentAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Bank_BankId",
                table: "Subcontractors",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Banks_BankId",
                table: "Vendors",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
