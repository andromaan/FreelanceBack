using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedProficiencyLevelToUserLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_languages_languages_languages_id",
                table: "users_languages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users_languages",
                table: "users_languages");

            migrationBuilder.DropIndex(
                name: "ix_users_languages_user_id",
                table: "users_languages");

            migrationBuilder.RenameColumn(
                name: "languages_id",
                table: "users_languages",
                newName: "language_id");

            migrationBuilder.AddColumn<string>(
                name: "proficiency_level",
                table: "users_languages",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users_languages",
                table: "users_languages",
                columns: new[] { "user_id", "language_id" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 20, 16, 18, 12, 866, DateTimeKind.Utc).AddTicks(1921), new DateTime(2026, 2, 20, 16, 18, 12, 866, DateTimeKind.Utc).AddTicks(1927), "BC6BCCCE69BAB78630A89CC85D2F56E381F40E95BF3EA8332C904F5F53F71C96-40AD3F19421F57885E401118B0EA9122" });

            migrationBuilder.CreateIndex(
                name: "ix_users_languages_language_id",
                table: "users_languages",
                column: "language_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_languages_languages_language_id",
                table: "users_languages",
                column: "language_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_languages_languages_language_id",
                table: "users_languages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users_languages",
                table: "users_languages");

            migrationBuilder.DropIndex(
                name: "ix_users_languages_language_id",
                table: "users_languages");

            migrationBuilder.DropColumn(
                name: "proficiency_level",
                table: "users_languages");

            migrationBuilder.RenameColumn(
                name: "language_id",
                table: "users_languages",
                newName: "languages_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users_languages",
                table: "users_languages",
                columns: new[] { "languages_id", "user_id" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 15, 19, 6, 37, 741, DateTimeKind.Utc).AddTicks(7553), new DateTime(2026, 2, 15, 19, 6, 37, 741, DateTimeKind.Utc).AddTicks(7559), "11EDDB55A343F1EA1EF2BCCB5BA82F4AB27F55D0C0AF3A562A893B831A1A3669-D332CC5FBB963F6E0A83CCA35158ED3D" });

            migrationBuilder.CreateIndex(
                name: "ix_users_languages_user_id",
                table: "users_languages",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_languages_languages_languages_id",
                table: "users_languages",
                column: "languages_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
