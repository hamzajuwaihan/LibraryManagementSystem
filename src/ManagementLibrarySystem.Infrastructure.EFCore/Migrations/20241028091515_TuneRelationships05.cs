using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementLibrarySystem.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class TuneRelationships05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMembers_Libraries_LibrariesId",
                table: "LibraryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMembers_Members_MembersId",
                table: "LibraryMembers");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "LibraryMembers",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "LibrariesId",
                table: "LibraryMembers",
                newName: "LibraryId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryMembers_MembersId",
                table: "LibraryMembers",
                newName: "IX_LibraryMembers_MemberId");

            migrationBuilder.AddColumn<Guid>(
                name: "LibraryId1",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LibraryMember",
                columns: table => new
                {
                    LibrariesId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryMember", x => new { x.LibrariesId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_LibraryMember_Libraries_LibrariesId",
                        column: x => x.LibrariesId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books",
                column: "BorrowedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryId1",
                table: "Books",
                column: "LibraryId1");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryMember_MembersId",
                table: "LibraryMember",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Libraries_LibraryId1",
                table: "Books",
                column: "LibraryId1",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books",
                column: "BorrowedBy",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMembers_Libraries_LibraryId",
                table: "LibraryMembers",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMembers_Members_MemberId",
                table: "LibraryMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Libraries_LibraryId1",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMembers_Libraries_LibraryId",
                table: "LibraryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMembers_Members_MemberId",
                table: "LibraryMembers");

            migrationBuilder.DropTable(
                name: "LibraryMember");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibraryId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibraryId1",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "LibraryMembers",
                newName: "MembersId");

            migrationBuilder.RenameColumn(
                name: "LibraryId",
                table: "LibraryMembers",
                newName: "LibrariesId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryMembers_MemberId",
                table: "LibraryMembers",
                newName: "IX_LibraryMembers_MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMembers_Libraries_LibrariesId",
                table: "LibraryMembers",
                column: "LibrariesId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMembers_Members_MembersId",
                table: "LibraryMembers",
                column: "MembersId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
