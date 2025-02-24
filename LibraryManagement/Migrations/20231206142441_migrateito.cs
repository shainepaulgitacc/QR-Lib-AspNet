using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class migrateito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookNumber",
                table: "BorrowedBooks",
                newName: "Year");

            migrationBuilder.AddColumn<string>(
                name: "AccessionNumber",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pages",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Volume",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessionNumber",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "BorrowedBooks",
                newName: "BookNumber");
        }
    }
}
