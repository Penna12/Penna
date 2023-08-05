using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class CurrentAccountAddToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    AuthorizedPerson = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    TownId = table.Column<int>(nullable: false),
                    TaxId = table.Column<string>(maxLength: 11, nullable: false),
                    TaxOffice = table.Column<string>(maxLength: 50, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 30, nullable: false),
                    Phone2 = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    BankId = table.Column<int>(nullable: true),
                    IBAN_Code = table.Column<string>(maxLength: 2, nullable: true),
                    IBAN = table.Column<string>(maxLength: 24, nullable: true),
                    BusinessGroupId = table.Column<byte>(nullable: true),
                    AccountTypeId = table.Column<byte>(nullable: false),
                    CompanyStatusId = table.Column<byte>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_BankId",
                table: "CurrentAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_CityId",
                table: "CurrentAccounts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_CountryId",
                table: "CurrentAccounts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_TenantId",
                table: "CurrentAccounts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_TownId",
                table: "CurrentAccounts",
                column: "TownId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentAccounts");
        }
    }
}
