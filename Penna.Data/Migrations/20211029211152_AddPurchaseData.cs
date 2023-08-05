using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class AddPurchaseData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Priority = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    PurchaseRequestId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    RequestedDeliveryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SupplierCurrentAccoutId = table.Column<int>(nullable: false),
                    PurchaseType = table.Column<byte>(nullable: false),
                    FinalBidDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequestedBlockId = table.Column<int>(nullable: false),
                    RequestedPlace = table.Column<string>(maxLength: 100, nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 10, nullable: true),
                    InvoiceAmount = table.Column<double>(nullable: false),
                    PurchaseStatus = table.Column<byte>(nullable: false),
                    Delivered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_PurchaseRequest_PurchaseRequestId",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Block_RequestedBlockId",
                        column: x => x.RequestedBlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_CurrentAccount_SupplierCurrentAccoutId",
                        column: x => x.SupplierCurrentAccoutId,
                        principalTable: "CurrentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseTender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PurchaseId = table.Column<int>(nullable: false),
                    SupplierCurrentAccoutId = table.Column<int>(nullable: false),
                    OfferTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Joined = table.Column<bool>(nullable: false),
                    Won = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseTenders_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseTenders_CurrentAccount_SupplierCurrentAccoutId",
                        column: x => x.SupplierCurrentAccoutId,
                        principalTable: "CurrentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_ProductId",
                table: "Purchase",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_ProjectId",
                table: "Purchase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PurchaseRequestId",
                table: "Purchase",
                column: "PurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_RequestedBlockId",
                table: "Purchase",
                column: "RequestedBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_SupplierCurrentAccoutId",
                table: "Purchase",
                column: "SupplierCurrentAccoutId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequest_ProductId",
                table: "PurchaseRequest",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequest_ProjectId",
                table: "PurchaseRequest",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTender_PurchaseId",
                table: "PurchaseTender",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTender_SupplierCurrentAccoutId",
                table: "PurchaseTender",
                column: "SupplierCurrentAccoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseTender");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "PurchaseRequest");
        }
    }
}
