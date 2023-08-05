using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class CurrentAccountAddToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentAccountId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentAccountId",
                table: "AspNetUsers",
                column: "CurrentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CurrentAccounts_CurrentAccountId",
                table: "AspNetUsers",
                column: "CurrentAccountId",
                principalTable: "CurrentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CurrentAccounts_CurrentAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentAccountId",
                table: "AspNetUsers");
        }
    }
}
