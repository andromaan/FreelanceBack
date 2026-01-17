using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedJobToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_job_job_id",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_proposals_jobs_job_id",
                table: "proposals");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.RenameColumn(
                name: "job_id",
                table: "proposals",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "ix_proposals_job_id",
                table: "proposals",
                newName: "ix_proposals_project_id");

            migrationBuilder.RenameColumn(
                name: "job_id",
                table: "contracts",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "ix_contracts_job_id",
                table: "contracts",
                newName: "ix_contracts_project_id");

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    employer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    budget_min = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    budget_max = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    is_hourly = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                    table.ForeignKey(
                        name: "fk_projects_employers_employer_id",
                        column: x => x.employer_id,
                        principalTable: "employers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_projects_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_projects_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 17, 14, 9, 24, 705, DateTimeKind.Utc).AddTicks(8541), new DateTime(2026, 1, 17, 14, 9, 24, 705, DateTimeKind.Utc).AddTicks(8547), "B6E6E8E1BC9FBC582541C9D4FFCA70B6A0C45CE649D657EF1B50BC945413E6AE-6F94EBF0475433A1D3DE2E373C24A60E" });

            migrationBuilder.CreateIndex(
                name: "ix_projects_created_by",
                table: "projects",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_projects_employer_id",
                table: "projects",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_modified_by",
                table: "projects",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_project_project_id",
                table: "contracts",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_proposals_projects_project_id",
                table: "proposals",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_project_project_id",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_proposals_projects_project_id",
                table: "proposals");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "proposals",
                newName: "job_id");

            migrationBuilder.RenameIndex(
                name: "ix_proposals_project_id",
                table: "proposals",
                newName: "ix_proposals_job_id");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "contracts",
                newName: "job_id");

            migrationBuilder.RenameIndex(
                name: "ix_contracts_project_id",
                table: "contracts",
                newName: "ix_contracts_job_id");

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    employer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    budget_max = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    budget_min = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    is_hourly = table.Column<bool>(type: "boolean", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jobs", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobs_employers_employer_id",
                        column: x => x.employer_id,
                        principalTable: "employers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_jobs_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_jobs_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 18, 37, 491, DateTimeKind.Utc).AddTicks(2874), new DateTime(2026, 1, 17, 11, 18, 37, 491, DateTimeKind.Utc).AddTicks(2881), "73B6E34C220F5D20A3F234085E9C17FECC825DE9356BCC6EA82676B5C9E32822-06FEDFB61DC414A30E0FE7E57985F531" });

            migrationBuilder.CreateIndex(
                name: "ix_jobs_created_by",
                table: "jobs",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_jobs_employer_id",
                table: "jobs",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "ix_jobs_modified_by",
                table: "jobs",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_job_job_id",
                table: "contracts",
                column: "job_id",
                principalTable: "jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_proposals_jobs_job_id",
                table: "proposals",
                column: "job_id",
                principalTable: "jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
