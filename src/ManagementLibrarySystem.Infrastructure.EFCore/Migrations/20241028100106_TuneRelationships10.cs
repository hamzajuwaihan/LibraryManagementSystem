using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementLibrarySystem.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class TuneRelationships10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_MemberId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_MemberId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Books");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_MemberId",
                table: "Books",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_MemberId",
                table: "Books",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
