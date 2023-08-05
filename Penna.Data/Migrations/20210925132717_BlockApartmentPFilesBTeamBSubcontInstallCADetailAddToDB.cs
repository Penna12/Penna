using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class BlockApartmentPFilesBTeamBSubcontInstallCADetailAddToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Banks_BankId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Cities_CityId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Countries_CountryId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Tenants_TenantId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Towns_TownId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Banks_BankId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Cities_CityId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Countries_CountryId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Tenants_TenantId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Towns_TownId",
                table: "Subcontractors");

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    TypeId = table.Column<byte>(nullable: false),
                    FloorCount = table.Column<byte>(nullable: false),
                    BasementCount = table.Column<byte>(nullable: false),
                    ApartmentCount = table.Column<int>(nullable: false),
                    CommercialCount = table.Column<int>(nullable: false),
                    BlockCostCalculation = table.Column<byte>(nullable: false),
                    ApartmentCostCalculation = table.Column<byte>(nullable: false),
                    PublicAreaCostCalculation = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    IntallmentNo = table.Column<byte>(nullable: false),
                    InstallmentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Payment = table.Column<double>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Floor = table.Column<int>(nullable: false),
                    SectionName = table.Column<string>(maxLength: 20, nullable: false),
                    Gross = table.Column<float>(nullable: false),
                    Net = table.Column<float>(nullable: false),
                    Gabari = table.Column<float>(nullable: false),
                    CurrentAccountId = table.Column<int>(nullable: false),
                    BlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartments_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apartments_CurrentAccount_CurrentAccountId",
                        column: x => x.CurrentAccountId,
                        principalTable: "CurrentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockSubcontractors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CurrentAccountId = table.Column<int>(nullable: false),
                    CompanyRepresentative = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 30, nullable: false),
                    BusinessGroupId = table.Column<byte>(nullable: false),
                    BlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockSubcontractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockSubcontractor_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockSubcontractors_CurrentAccount_CurrentAccountId",
                        column: x => x.CurrentAccountId,
                        principalTable: "CurrentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    Phone = table.Column<string>(maxLength: 30, nullable: false),
                    ProjectPositionId = table.Column<int>(nullable: false),
                    BlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockTeams_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CurrentAccountId = table.Column<int>(nullable: false),
                    CurDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Debit = table.Column<double>(nullable: false),
                    Credit = table.Column<double>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    InstallmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccountDetails_CurrentAccount_CurrentAccountId",
                        column: x => x.CurrentAccountId,
                        principalTable: "CurrentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentAccountDetails_Installment_InstallmentId",
                        column: x => x.InstallmentId,
                        principalTable: "Installments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentAccountDetails_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    FilePath = table.Column<string>(maxLength: 200, nullable: false),
                    FileTypeId = table.Column<byte>(nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    BlockId = table.Column<int>(nullable: false),
                    ApartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFiles_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectFiles_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectFiles_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_BlockId",
                table: "Apartments",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CurrentAccountId",
                table: "Apartments",
                column: "CurrentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ProjectId",
                table: "Blocks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockSubcontractors_BlockId",
                table: "BlockSubcontractors",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockSubcontractors_CurrentAccountId",
                table: "BlockSubcontractors",
                column: "CurrentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockTeams_BlockId",
                table: "BlockTeams",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountDetails_CurrentAccountId",
                table: "CurrentAccountDetails",
                column: "CurrentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountDetails_InstallmentId",
                table: "CurrentAccountDetails",
                column: "InstallmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountDetails_ProjectId",
                table: "CurrentAccountDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_ProjectId",
                table: "Installments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ApartmentId",
                table: "ProjectFiles",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_BlockId",
                table: "ProjectFiles",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ProjectId",
                table: "ProjectFiles",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Bank_BankId",
                table: "CurrentAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_City_CityId",
                table: "CurrentAccounts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Country_CountryId",
                table: "CurrentAccounts",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Tenant_TenantId",
                table: "CurrentAccounts",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Town_TownId",
                table: "CurrentAccounts",
                column: "TownId",
                principalTable: "Towns",
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
                name: "FK_Subcontractors_City_CityId",
                table: "Subcontractors",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Country_CountryId",
                table: "Subcontractors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Tenant_TenantId",
                table: "Subcontractors",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Town_TownId",
                table: "Subcontractors",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Bank_BankId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_City_CityId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Country_CountryId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Tenant_TenantId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Town_TownId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Bank_BankId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_City_CityId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Country_CountryId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Tenant_TenantId",
                table: "Subcontractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcontractors_Town_TownId",
                table: "Subcontractors");

            migrationBuilder.DropTable(
                name: "BlockSubcontractors");

            migrationBuilder.DropTable(
                name: "BlockTeams");

            migrationBuilder.DropTable(
                name: "CurrentAccountDetails");

            migrationBuilder.DropTable(
                name: "ProjectFiles");

            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Banks_BankId",
                table: "CurrentAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Cities_CityId",
                table: "CurrentAccounts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Countries_CountryId",
                table: "CurrentAccounts",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Tenants_TenantId",
                table: "CurrentAccounts",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Towns_TownId",
                table: "CurrentAccounts",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Banks_BankId",
                table: "Subcontractors",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Cities_CityId",
                table: "Subcontractors",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Countries_CountryId",
                table: "Subcontractors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Tenants_TenantId",
                table: "Subcontractors",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontractors_Towns_TownId",
                table: "Subcontractors",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
