using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class CreateCashMoviment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashDescks_User_UserId",
                table: "CashDescks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashDescks",
                table: "CashDescks");

            migrationBuilder.RenameTable(
                name: "CashDescks",
                newName: "CashDesck");

            migrationBuilder.RenameIndex(
                name: "IX_CashDescks_UserId",
                table: "CashDesck",
                newName: "IX_CashDesck_UserId");

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningAmount",
                table: "CashDesck",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CashDesck",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashDesck",
                table: "CashDesck",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashDesck_User_UserId",
                table: "CashDesck",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashDesck_User_UserId",
                table: "CashDesck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashDesck",
                table: "CashDesck");

            migrationBuilder.DropColumn(
                name: "OpeningAmount",
                table: "CashDesck");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CashDesck");

            migrationBuilder.RenameTable(
                name: "CashDesck",
                newName: "CashDescks");

            migrationBuilder.RenameIndex(
                name: "IX_CashDesck_UserId",
                table: "CashDescks",
                newName: "IX_CashDescks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashDescks",
                table: "CashDescks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashDescks_User_UserId",
                table: "CashDescks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
