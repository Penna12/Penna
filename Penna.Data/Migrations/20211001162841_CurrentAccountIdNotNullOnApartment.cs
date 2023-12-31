﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class CurrentAccountIdNotNullOnApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrentAccountId",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrentAccountId",
                table: "Apartments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
