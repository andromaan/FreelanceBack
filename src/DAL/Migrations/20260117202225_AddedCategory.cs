using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_proposals_projects_project_id",
                table: "proposals");

            migrationBuilder.DropForeignKey(
                name: "fk_user_skills_skills_skill_id",
                table: "user_skills");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 17, 20, 22, 25, 518, DateTimeKind.Utc).AddTicks(9454), new DateTime(2026, 1, 17, 20, 22, 25, 518, DateTimeKind.Utc).AddTicks(9460), "400AB221E8AD5A194A468573B67978775AD35374E0285F77CE0BBEAE54C9F55A-9E24EB3E5079D40B8276D27D524F0107" });

            migrationBuilder.AddForeignKey(
                name: "fk_proposals_project_project_id",
                table: "proposals",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_user_skills_skill_skill_id",
                table: "user_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_proposals_project_project_id",
                table: "proposals");

            migrationBuilder.DropForeignKey(
                name: "fk_user_skills_skill_skill_id",
                table: "user_skills");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 17, 14, 9, 24, 705, DateTimeKind.Utc).AddTicks(8541), new DateTime(2026, 1, 17, 14, 9, 24, 705, DateTimeKind.Utc).AddTicks(8547), "B6E6E8E1BC9FBC582541C9D4FFCA70B6A0C45CE649D657EF1B50BC945413E6AE-6F94EBF0475433A1D3DE2E373C24A60E" });

            migrationBuilder.AddForeignKey(
                name: "fk_proposals_projects_project_id",
                table: "proposals",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_user_skills_skills_skill_id",
                table: "user_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
