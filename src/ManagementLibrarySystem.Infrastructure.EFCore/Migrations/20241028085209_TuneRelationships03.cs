using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementLibrarySystem.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class TuneRelationships03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMember_Libraries_LibrariesId",
                table: "LibraryMember");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMember_Members_MembersId",
                table: "LibraryMember");

            migrationBuilder.DropTable(
                name: "BookLibrary");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember");

            migrationBuilder.RenameTable(
                name: "LibraryMember",
                newName: "LibraryMembers");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryMember_MembersId",
                table: "LibraryMembers",
                newName: "IX_LibraryMembers_MembersId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Members",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Libraries",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AddColumn<Guid>(
                name: "LibraryId",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryMembers",
                table: "LibraryMembers",
                columns: new[] { "LibrariesId", "MembersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_MemberId",
                table: "Books",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Libraries_Id",
                table: "Books",
                column: "Id",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_MemberId",
                table: "Books",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Libraries_Id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_MemberId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMembers_Libraries_LibrariesId",
                table: "LibraryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMembers_Members_MembersId",
                table: "LibraryMembers");

            migrationBuilder.DropIndex(
                name: "IX_Books_MemberId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryMembers",
                table: "LibraryMembers");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "LibraryMembers",
                newName: "LibraryMember");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryMembers_MembersId",
                table: "LibraryMember",
                newName: "IX_LibraryMember_MembersId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Libraries",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember",
                columns: new[] { "LibrariesId", "MembersId" });

            migrationBuilder.CreateTable(
                name: "BookLibrary",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false),
                    LibrariesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLibrary", x => new { x.BooksId, x.LibrariesId });
                    table.ForeignKey(
                        name: "FK_BookLibrary_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLibrary_Libraries_LibrariesId",
                        column: x => x.LibrariesId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowedBy",
                table: "Books",
                column: "BorrowedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookLibrary_LibrariesId",
                table: "BookLibrary",
                column: "LibrariesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_BorrowedBy",
                table: "Books",
                column: "BorrowedBy",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMember_Libraries_LibrariesId",
                table: "LibraryMember",
                column: "LibrariesId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMember_Members_MembersId",
                table: "LibraryMember",
                column: "MembersId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
