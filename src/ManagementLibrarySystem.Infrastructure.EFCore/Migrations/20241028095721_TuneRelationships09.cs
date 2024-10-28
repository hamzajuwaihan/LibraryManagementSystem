using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementLibrarySystem.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class TuneRelationships09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books",
                column: "BorrowedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books",
                column: "BorrowedBy",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
