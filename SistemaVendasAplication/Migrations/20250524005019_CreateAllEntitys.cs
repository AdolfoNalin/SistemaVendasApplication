using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendasAplication.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllEntitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShotName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RG = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CPF = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    CEP = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    City = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Number = table.Column<int>(type: "integer", maxLength: 12, nullable: true),
                    Neighborhoods = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    State = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShotName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RG = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CPF = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    CEP = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    City = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Number = table.Column<int>(type: "integer", maxLength: 12, nullable: true),
                    Neighborhoods = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    State = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Login = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Authorizations = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSupllier = table.Column<Guid>(type: "uuid", nullable: false),
                    FullDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CashPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    TermPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    entryPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Amount = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    UnitMeasure = table.Column<string>(type: "text", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShotName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RG = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CPF = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    CNPJ = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    TelephoneNumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    CEP = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    City = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Number = table.Column<int>(type: "integer", maxLength: 12, nullable: true),
                    Neighborhoods = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    State = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClient = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    DescountPorcentage = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CashDescount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AdditionPorcentage = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AdditionCash = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Changes = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Budget_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClient = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    DescountPorcentage = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CashDescount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AdditionPorcentage = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AdditionCash = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Changes = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sale_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemBudget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdBudget = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBudget_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budget",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemBudget_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemReturn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSale = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClient = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    Reason = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Obs = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReturn_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReturn_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReturn_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemSale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSale_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemSale_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget_ClientId",
                table: "Budget",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_EmployeeId",
                table: "Budget",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBudget_BudgetId",
                table: "ItemBudget",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBudget_ProductId",
                table: "ItemBudget",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReturn_ClientId",
                table: "ItemReturn",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReturn_ProductId",
                table: "ItemReturn",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReturn_SaleId",
                table: "ItemReturn",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSale_ProductId",
                table: "ItemSale",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSale_SaleId",
                table: "ItemSale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ClientId",
                table: "Sale",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_EmployeeId",
                table: "Sale",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemBudget");

            migrationBuilder.DropTable(
                name: "ItemReturn");

            migrationBuilder.DropTable(
                name: "ItemSale");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
