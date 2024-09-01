using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionLogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GLTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolderAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "MONEY", nullable: false),
                    From = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    To = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    TransactionType = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GLTransactions_HolderAccounts_HolderAccountId",
                        column: x => x.HolderAccountId,
                        principalTable: "HolderAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GLTransactions_HolderAccountId",
                table: "GLTransactions",
                column: "HolderAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GLTransactions");
        }
    }
}
