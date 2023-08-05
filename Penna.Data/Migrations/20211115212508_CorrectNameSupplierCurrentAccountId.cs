using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class CorrectNameSupplierCurrentAccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_CurrentAccount_SupplierCurrentAccoutId",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseTenders_CurrentAccount_SupplierCurrentAccoutId",
                table: "PurchaseTender");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseTender_SupplierCurrentAccoutId",
                table: "PurchaseTender");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_SupplierCurrentAccoutId",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "SupplierCurrentAccoutId",
                table: "PurchaseTender");

            migrationBuilder.DropColumn(
                name: "SupplierCurrentAccoutId",
                table: "Purchase");

            migrationBuilder.AddColumn<int>(
                name: "SupplierCurrentAccountId",
                table: "PurchaseTender",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierCurrentAccountId",
                table: "Purchase",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTender_SupplierCurrentAccountId",
                table: "PurchaseTender",
                column: "SupplierCurrentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_SupplierCurrentAccountId",
                table: "Purchase",
                column: "SupplierCurrentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_CurrentAccount_SupplierCurrentAccountId",
                table: "Purchase",
                column: "SupplierCurrentAccountId",
                principalTable: "CurrentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseTenders_CurrentAccount_SupplierCurrentAccountId",
                table: "PurchaseTender",
                column: "SupplierCurrentAccountId",
                principalTable: "CurrentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_CurrentAccount_SupplierCurrentAccountId",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseTenders_CurrentAccount_SupplierCurrentAccountId",
                table: "PurchaseTender");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseTender_SupplierCurrentAccountId",
                table: "PurchaseTender");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_SupplierCurrentAccountId",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "SupplierCurrentAccountId",
                table: "PurchaseTender");

            migrationBuilder.DropColumn(
                name: "SupplierCurrentAccountId",
                table: "Purchase");

            migrationBuilder.AddColumn<int>(
                name: "SupplierCurrentAccoutId",
                table: "PurchaseTender",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierCurrentAccoutId",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTender_SupplierCurrentAccoutId",
                table: "PurchaseTender",
                column: "SupplierCurrentAccoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_SupplierCurrentAccoutId",
                table: "Purchase",
                column: "SupplierCurrentAccoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_CurrentAccount_SupplierCurrentAccoutId",
                table: "Purchase",
                column: "SupplierCurrentAccoutId",
                principalTable: "CurrentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseTenders_CurrentAccount_SupplierCurrentAccoutId",
                table: "PurchaseTender",
                column: "SupplierCurrentAccoutId",
                principalTable: "CurrentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
