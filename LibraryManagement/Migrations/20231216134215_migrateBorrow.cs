using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class migrateBorrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_BookCategories_Category",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_User",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "AccessionNumber",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BookTitle",
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

            migrationBuilder.DropColumn(
                name: "Year",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "BorrowedBooks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "BorrowedBooks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_User",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_Category",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_BookId");

            migrationBuilder.AddColumn<int>(
                name: "BookCategoryId",
                table: "BorrowedBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookCategoryId",
                table: "BorrowedBooks",
                column: "BookCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_BookCategories_BookCategoryId",
                table: "BorrowedBooks",
                column: "BookCategoryId",
                principalTable: "BookCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_UserId",
                table: "BorrowedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_BookCategories_BookCategoryId",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_UserId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_BookCategoryId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BookCategoryId",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BorrowedBooks",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BorrowedBooks",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_UserId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_User");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_Category");

            migrationBuilder.AddColumn<string>(
                name: "AccessionNumber",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
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

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_BookCategories_Category",
                table: "BorrowedBooks",
                column: "Category",
                principalTable: "BookCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_User",
                table: "BorrowedBooks",
                column: "User",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
