using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoriesToProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_freelancers_info_freelancer_id",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancer_language_freelancers_info_freelancer_id",
                table: "freelancer_language");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_info_country_country_id",
                table: "freelancers_info");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_info_users_created_by",
                table: "freelancers_info");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_info_users_modified_by",
                table: "freelancers_info");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_info_users_user_id",
                table: "freelancers_info");

            migrationBuilder.DropForeignKey(
                name: "fk_portfolio_items_freelancers_info_freelancer_id",
                table: "portfolio_items");

            migrationBuilder.DropForeignKey(
                name: "fk_projects_employers_employer_id",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "fk_proposals_freelancers_info_freelancer_id",
                table: "proposals");

            migrationBuilder.DropTable(
                name: "user_skills");

            migrationBuilder.DropIndex(
                name: "ix_projects_employer_id",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "pk_freelancers_info",
                table: "freelancers_info");

            migrationBuilder.DropColumn(
                name: "category",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "employer_id",
                table: "projects");

            migrationBuilder.RenameTable(
                name: "freelancers_info",
                newName: "freelancers");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_info_user_id",
                table: "freelancers",
                newName: "ix_freelancers_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_info_modified_by",
                table: "freelancers",
                newName: "ix_freelancers_modified_by");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_info_created_by",
                table: "freelancers",
                newName: "ix_freelancers_created_by");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_info_country_id",
                table: "freelancers",
                newName: "ix_freelancers_country_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_freelancers",
                table: "freelancers",
                column: "id");

            migrationBuilder.CreateTable(
                name: "category_project",
                columns: table => new
                {
                    categories_id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_project", x => new { x.categories_id, x.project_id });
                    table.ForeignKey(
                        name: "fk_category_project_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_project_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "freelancers_skills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freelancers_skills", x => x.id);
                    table.ForeignKey(
                        name: "fk_freelancers_skills_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_freelancers_skills_skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 20, 12, 0, 51, 958, DateTimeKind.Utc).AddTicks(3577), new DateTime(2026, 1, 20, 12, 0, 51, 958, DateTimeKind.Utc).AddTicks(3586), "648F71DB31C58420563B6F1022799EACEC677B0767FE2DDC8093D23E55AD5C82-62FEA1406A384136AB2560C3E60A767B" });

            migrationBuilder.CreateIndex(
                name: "ix_category_project_project_id",
                table: "category_project",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_skills_freelancer_id",
                table: "freelancers_skills",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_skills_skill_id",
                table: "freelancers_skills",
                column: "skill_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_freelancers_freelancer_id",
                table: "contracts",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancer_language_freelancers_freelancer_id",
                table: "freelancer_language",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_country_country_id",
                table: "freelancers",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_created_by",
                table: "freelancers",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_modified_by",
                table: "freelancers",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_user_id",
                table: "freelancers",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_portfolio_items_freelancers_freelancer_id",
                table: "portfolio_items",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proposals_freelancers_freelancer_id",
                table: "proposals",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_freelancers_freelancer_id",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancer_language_freelancers_freelancer_id",
                table: "freelancer_language");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_country_country_id",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_created_by",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_modified_by",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_user_id",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_portfolio_items_freelancers_freelancer_id",
                table: "portfolio_items");

            migrationBuilder.DropForeignKey(
                name: "fk_proposals_freelancers_freelancer_id",
                table: "proposals");

            migrationBuilder.DropTable(
                name: "category_project");

            migrationBuilder.DropTable(
                name: "freelancers_skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_freelancers",
                table: "freelancers");

            migrationBuilder.RenameTable(
                name: "freelancers",
                newName: "freelancers_info");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_user_id",
                table: "freelancers_info",
                newName: "ix_freelancers_info_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_modified_by",
                table: "freelancers_info",
                newName: "ix_freelancers_info_modified_by");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_created_by",
                table: "freelancers_info",
                newName: "ix_freelancers_info_created_by");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_country_id",
                table: "freelancers_info",
                newName: "ix_freelancers_info_country_id");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "projects",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "employer_id",
                table: "projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_freelancers_info",
                table: "freelancers_info",
                column: "id");

            migrationBuilder.CreateTable(
                name: "user_skills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_skills", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_skills_freelancers_info_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_skills_skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 17, 20, 22, 25, 518, DateTimeKind.Utc).AddTicks(9454), new DateTime(2026, 1, 17, 20, 22, 25, 518, DateTimeKind.Utc).AddTicks(9460), "400AB221E8AD5A194A468573B67978775AD35374E0285F77CE0BBEAE54C9F55A-9E24EB3E5079D40B8276D27D524F0107" });

            migrationBuilder.CreateIndex(
                name: "ix_projects_employer_id",
                table: "projects",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_skills_freelancer_id",
                table: "user_skills",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_skills_skill_id",
                table: "user_skills",
                column: "skill_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_freelancers_info_freelancer_id",
                table: "contracts",
                column: "freelancer_id",
                principalTable: "freelancers_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancer_language_freelancers_info_freelancer_id",
                table: "freelancer_language",
                column: "freelancer_id",
                principalTable: "freelancers_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_info_country_country_id",
                table: "freelancers_info",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_info_users_created_by",
                table: "freelancers_info",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_info_users_modified_by",
                table: "freelancers_info",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_info_users_user_id",
                table: "freelancers_info",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_portfolio_items_freelancers_info_freelancer_id",
                table: "portfolio_items",
                column: "freelancer_id",
                principalTable: "freelancers_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_projects_employers_employer_id",
                table: "projects",
                column: "employer_id",
                principalTable: "employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_proposals_freelancers_info_freelancer_id",
                table: "proposals",
                column: "freelancer_id",
                principalTable: "freelancers_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
