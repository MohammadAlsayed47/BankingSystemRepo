using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteStateEnumForHolderAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeleteState",
                table: "HolderAccounts",
                type: "VARCHAR(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "NotDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteState",
                table: "HolderAccounts");
        }
    }
}
