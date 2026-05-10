using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankacılılSistemi_DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTransactionTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transcations",
                table: "Transcations");

            migrationBuilder.RenameTable(
                name: "Transcations",
                newName: "Transactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transcations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transcations",
                table: "Transcations",
                column: "Id");
        }
    }
}
