using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClient_complement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Client",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Client");
        }
    }
}
