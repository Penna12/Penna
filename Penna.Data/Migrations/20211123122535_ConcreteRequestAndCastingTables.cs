using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class ConcreteRequestAndCastingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConcreteRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    BlockId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcreteRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcreteRequests_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConcreteRequests_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConcreteCasting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    ConcreteRequestId = table.Column<int>(nullable: false),
                    CarPlate = table.Column<string>(maxLength: 15, nullable: false),
                    CastingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    WaybilNumber = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcreteCasting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcreteCasting_ConcreteRequest_ConcreteRequestId",
                        column: x => x.ConcreteRequestId,
                        principalTable: "ConcreteRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcreteCasting_ConcreteRequestId",
                table: "ConcreteCasting",
                column: "ConcreteRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcreteRequest_BlockId",
                table: "ConcreteRequest",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcreteRequest_ProductId",
                table: "ConcreteRequest",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcreteCasting");

            migrationBuilder.DropTable(
                name: "ConcreteRequest");
        }
    }
}
