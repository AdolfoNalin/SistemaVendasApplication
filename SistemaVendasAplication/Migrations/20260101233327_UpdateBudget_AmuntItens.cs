using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBudget_AmuntItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescountPorcentage",
                table: "Budget",
                newName: "DescountPercentage");

            migrationBuilder.RenameColumn(
                name: "AdditionPorcentage",
                table: "Budget",
                newName: "AdditionPercentage");

            migrationBuilder.AddColumn<int>(
                name: "AmountItens",
                table: "Budget",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountItens",
                table: "Budget");

            migrationBuilder.RenameColumn(
                name: "DescountPercentage",
                table: "Budget",
                newName: "DescountPorcentage");

            migrationBuilder.RenameColumn(
                name: "AdditionPercentage",
                table: "Budget",
                newName: "AdditionPorcentage");
        }
    }
}
