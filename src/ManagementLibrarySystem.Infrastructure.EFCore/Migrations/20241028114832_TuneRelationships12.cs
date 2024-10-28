using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementLibrarySystem.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class TuneRelationships12 : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryMembers",
                table: "LibraryMembers");

            migrationBuilder.RenameTable(
                name: "LibraryMembers",
                newName: "LibraryMember");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryMembers_MembersId",
                table: "LibraryMember",
                newName: "IX_LibraryMember_MembersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember",
                columns: new[] { "LibrariesId", "MembersId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMember_Libraries_LibrariesId",
                table: "LibraryMember");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMember_Members_MembersId",
                table: "LibraryMember");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryMembers",
                table: "LibraryMembers",
                columns: new[] { "LibrariesId", "MembersId" });

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
