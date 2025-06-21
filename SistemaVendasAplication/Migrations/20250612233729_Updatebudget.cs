using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class Updatebudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemBudget_Budget_BudgetId1",
                table: "ItemBudget");

            migrationBuilder.DropIndex(
                name: "IX_ItemBudget_BudgetId1",
                table: "ItemBudget");

            migrationBuilder.DropColumn(
                name: "BudgetId1",
                table: "ItemBudget");

            migrationBuilder.DropColumn(
                name: "ItemBudgetId",
                table: "Budget");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget",
                column: "BudgetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBudget_Budget_BudgetId",
                table: "ItemBudget",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemBudget_Budget_BudgetId",
                table: "ItemBudget");

            migrationBuilder.DropIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget");

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetId1",
                table: "ItemBudget",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemBudgetId",
                table: "Budget",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ItemBudget_BudgetId1",
                table: "ItemBudget",
                column: "BudgetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBudget_Budget_BudgetId1",
                table: "ItemBudget",
                column: "BudgetId1",
                principalTable: "Budget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
