using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget");

            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "Sale");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget",
                column: "BudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget");

            migrationBuilder.AddColumn<Guid>(
                name: "IdProduct",
                table: "Sale",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget",
                column: "BudgetId",
                unique: true);
        }
    }
}
