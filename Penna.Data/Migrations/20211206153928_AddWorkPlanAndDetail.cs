using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class AddWorkPlanAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    WorkName = table.Column<string>(maxLength: 100, nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    BusinessGroup = table.Column<byte>(nullable: false),
                    ContractorCurrentAccountId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlans_CurrentAccount_ContractorCurrentAccountId",
                        column: x => x.ContractorCurrentAccountId,
                        principalTable: "CurrentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    WorkPlanId = table.Column<int>(nullable: false),
                    ParentWorkPlanDetailId = table.Column<int>(nullable: true),
                    PlanDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AssignedWorkAmount = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    DoneWorkAmount = table.Column<int>(nullable: false),
                    RefuseWorkAmount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlanDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlanDetail_WorkPlanDetail_ParentWorkPlanDetailId",
                        column: x => x.ParentWorkPlanDetailId,
                        principalTable: "WorkPlanDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanDetails_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanDetails_WorkPlan_WorkPlanId",
                        column: x => x.WorkPlanId,
                        principalTable: "WorkPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlan_ContractorCurrentAccountId",
                table: "WorkPlan",
                column: "ContractorCurrentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlan_UnitId",
                table: "WorkPlan",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanDetail_ParentWorkPlanDetailId",
                table: "WorkPlanDetail",
                column: "ParentWorkPlanDetailId",
                unique: true,
                filter: "[ParentWorkPlanDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanDetail_UnitId",
                table: "WorkPlanDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanDetail_WorkPlanId",
                table: "WorkPlanDetail",
                column: "WorkPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkPlanDetail");

            migrationBuilder.DropTable(
                name: "WorkPlan");
        }
    }
}
