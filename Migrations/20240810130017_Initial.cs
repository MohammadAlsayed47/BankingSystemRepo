using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AccountSequence");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holders",
                columns: table => new
                {
                    NId = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    FaName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "DATE", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holders", x => x.NId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [AccountSequence]"),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    IsOnwer = table.Column<bool>(type: "bit", maxLength: 4, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAccounts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolderAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [AccountSequence]"),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "MONEY", nullable: false),
                    HolderNId = table.Column<string>(type: "VARCHAR(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolderAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolderAccounts_Holders_HolderNId",
                        column: x => x.HolderNId,
                        principalTable: "Holders",
                        principalColumn: "NId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "messi@gmail.com", "Messi" },
                    { 2, "ronaldinho@gmail.com", "Ronaldinho" },
                    { 3, "neymar@gmail.com", "Neymar" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeAccounts",
                columns: new[] { "Id", "CreatedAt", "EmployeeId", "IsOnwer", "Password", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "messi1010", "messi10" },
                    { 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, "ronaldinho1010", "ronaldinho10" },
                    { 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, "neymar1010", "neymar10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAccounts_EmployeeId",
                table: "EmployeeAccounts",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HolderAccounts_HolderNId",
                table: "HolderAccounts",
                column: "HolderNId",
                unique: true,
                filter: "[HolderNId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAccounts");

            migrationBuilder.DropTable(
                name: "HolderAccounts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Holders");

            migrationBuilder.DropSequence(
                name: "AccountSequence");
        }
    }
}
