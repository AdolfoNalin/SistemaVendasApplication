using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityUserLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "DescountPorcentage",
                table: "Sale",
                newName: "PorcentageDicount");

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Sale",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymenetMethod",
                table: "Sale",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UnitMeasure",
                table: "Product",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "PaymenetMethod",
                table: "Sale");

            migrationBuilder.RenameColumn(
                name: "PorcentageDicount",
                table: "Sale",
                newName: "DescountPorcentage");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Sale",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UnitMeasure",
                table: "Product",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
