using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    alpha2code = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    alpha3code = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skills", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    avatar_img = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    display_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    external_provider = table.Column<string>(type: "text", nullable: true),
                    external_provider_key = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_country_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_users_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_users_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "disputes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disputes", x => x.id);
                    table.ForeignKey(
                        name: "fk_disputes_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_disputes_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    company_website = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employers", x => x.id);
                    table.ForeignKey(
                        name: "fk_employers_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employers_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "freelancers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bio = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    location = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    avatar_logo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freelancers", x => x.id);
                    table.ForeignKey(
                        name: "fk_freelancers_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_freelancers_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    budget = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    token = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    jwt_id = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    expired_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_wallets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_wallets", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_wallets_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_wallets_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_languages",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false),
                    proficiency_level = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_languages", x => new { x.user_id, x.language_id });
                    table.ForeignKey(
                        name: "fk_users_languages_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_languages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dispute_resolutions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dispute_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resolution_details = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dispute_resolutions", x => x.id);
                    table.ForeignKey(
                        name: "fk_dispute_resolutions_disputes_dispute_id",
                        column: x => x.dispute_id,
                        principalTable: "disputes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dispute_resolutions_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_dispute_resolutions_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "freelancers_skills",
                columns: table => new
                {
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skills_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freelancers_skills", x => new { x.freelancer_id, x.skills_id });
                    table.ForeignKey(
                        name: "fk_freelancers_skills_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_freelancers_skills_skill_skills_id",
                        column: x => x.skills_id,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "portfolio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    portfolio_url = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_portfolio", x => x.id);
                    table.ForeignKey(
                        name: "fk_portfolio_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_portfolio_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_portfolio_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bids",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    message = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bids", x => x.id);
                    table.ForeignKey(
                        name: "fk_bids_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bids_project_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "timezone('utc', now())"),
                    agreed_rate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contracts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contracts_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_contracts_project_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contracts_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_contracts_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "project_milestones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_milestones", x => x.id);
                    table.ForeignKey(
                        name: "fk_project_milestones_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_milestones_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_project_milestones_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "projects_categories",
                columns: table => new
                {
                    categories_id = table.Column<int>(type: "integer", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects_categories", x => new { x.categories_id, x.project_id });
                    table.ForeignKey(
                        name: "fk_projects_categories_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_projects_categories_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    message = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quotes", x => x.id);
                    table.ForeignKey(
                        name: "fk_quotes_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quotes_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wallet_transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    wallet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    transaction_type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wallet_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_wallet_transactions_user_wallets_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "user_wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contract_milestones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_milestones", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_milestones_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contract_milestones_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contract_milestones_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: true),
                    receiver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messages_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_messages_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_messages_users_receiver_id",
                        column: x => x.receiver_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    payment_method = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reviewed_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false),
                    review_text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    reviewer_role_id = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_reviews_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reviews_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contract_payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    milestone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    payment_method = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_payments_contract_milestones_milestone_id",
                        column: x => x.milestone_id,
                        principalTable: "contract_milestones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contract_payments_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "alpha2code", "alpha3code", "name" },
                values: new object[,]
                {
                    { 1, "AF", "AFG", "Afghanistan" },
                    { 2, "AL", "ALB", "Albania" },
                    { 3, "DZ", "DZA", "Algeria" },
                    { 4, "AS", "ASM", "American Samoa" },
                    { 5, "AD", "AND", "Andorra" },
                    { 6, "AO", "AGO", "Angola" },
                    { 7, "AI", "AIA", "Anguilla" },
                    { 8, "AQ", "ATA", "Antarctica" },
                    { 9, "AG", "ATG", "Antigua and Barbuda" },
                    { 10, "AR", "ARG", "Argentina" },
                    { 11, "AM", "ARM", "Armenia" },
                    { 12, "AW", "ABW", "Aruba" },
                    { 13, "AU", "AUS", "Australia" },
                    { 14, "AT", "AUT", "Austria" },
                    { 15, "AZ", "AZE", "Azerbaijan" },
                    { 16, "BS", "BHS", "Bahamas" },
                    { 17, "BH", "BHR", "Bahrain" },
                    { 18, "BD", "BGD", "Bangladesh" },
                    { 19, "BB", "BRB", "Barbados" },
                    { 20, "BY", "BLR", "Belarus" },
                    { 21, "BE", "BEL", "Belgium" },
                    { 22, "BZ", "BLZ", "Belize" },
                    { 23, "BJ", "BEN", "Benin" },
                    { 24, "BM", "BMU", "Bermuda" },
                    { 25, "BT", "BTN", "Bhutan" },
                    { 26, "BO", "BOL", "Bolivia" },
                    { 27, "BQ", "BES", "Bonaire, Sint Eustatius and Saba" },
                    { 28, "BA", "BIH", "Bosnia and Herzegovina" },
                    { 29, "BW", "BWA", "Botswana" },
                    { 30, "BV", "BVT", "Bouvet Island" },
                    { 31, "BR", "BRA", "Brazil" },
                    { 32, "IO", "IOT", "British Indian Ocean Territory" },
                    { 33, "BN", "BRN", "Brunei Darussalam" },
                    { 34, "BG", "BGR", "Bulgaria" },
                    { 35, "BF", "BFA", "Burkina Faso" },
                    { 36, "BI", "BDI", "Burundi" },
                    { 37, "KH", "KHM", "Cambodia" },
                    { 38, "CM", "CMR", "Cameroon" },
                    { 39, "CA", "CAN", "Canada" },
                    { 40, "CV", "CPV", "Cape Verde" },
                    { 41, "KY", "CYM", "Cayman Islands" },
                    { 42, "CF", "CAF", "Central African Republic" },
                    { 43, "TD", "TCD", "Chad" },
                    { 44, "CL", "CHL", "Chile" },
                    { 45, "CN", "CHN", "China" },
                    { 46, "CX", "CXR", "Christmas Island" },
                    { 47, "CC", "CCK", "Cocos (Keeling) Islands" },
                    { 48, "CO", "COL", "Colombia" },
                    { 49, "KM", "COM", "Comoros" },
                    { 50, "CD", "COD", "Congo" },
                    { 51, "CG", "COG", "Congo" },
                    { 52, "CK", "COK", "Cook Islands" },
                    { 53, "CR", "CRI", "Costa Rica" },
                    { 54, "HR", "HRV", "Croatia" },
                    { 55, "CU", "CUB", "Cuba" },
                    { 56, "CW", "CUW", "Curaçao" },
                    { 57, "CY", "CYP", "Cyprus" },
                    { 58, "CZ", "CZE", "Czech Republic" },
                    { 59, "CI", "CIV", "Côte D'Ivoire" },
                    { 60, "DK", "DNK", "Denmark" },
                    { 61, "DJ", "DJI", "Djibouti" },
                    { 62, "DM", "DMA", "Dominica" },
                    { 63, "DO", "DOM", "Dominican Republic" },
                    { 64, "EC", "ECU", "Ecuador" },
                    { 65, "EG", "EGY", "Egypt" },
                    { 66, "SV", "SLV", "El Salvador" },
                    { 67, "GQ", "GNQ", "Equatorial Guinea" },
                    { 68, "ER", "ERI", "Eritrea" },
                    { 69, "EE", "EST", "Estonia" },
                    { 70, "ET", "ETH", "Ethiopia" },
                    { 71, "FK", "FLK", "Falkland Islands (Malvinas)" },
                    { 72, "FO", "FRO", "Faroe Islands" },
                    { 73, "FJ", "FJI", "Fiji" },
                    { 74, "FI", "FIN", "Finland" },
                    { 75, "FR", "FRA", "France" },
                    { 76, "GF", "GUF", "French Guiana" },
                    { 77, "PF", "PYF", "French Polynesia" },
                    { 78, "TF", "ATF", "French Southern Territories" },
                    { 79, "GA", "GAB", "Gabon" },
                    { 80, "GM", "GMB", "Gambia" },
                    { 81, "GE", "GEO", "Georgia" },
                    { 82, "DE", "DEU", "Germany" },
                    { 83, "GH", "GHA", "Ghana" },
                    { 84, "GI", "GIB", "Gibraltar" },
                    { 85, "GR", "GRC", "Greece" },
                    { 86, "GL", "GRL", "Greenland" },
                    { 87, "GD", "GRD", "Grenada" },
                    { 88, "GP", "GLP", "Guadeloupe" },
                    { 89, "GU", "GUM", "Guam" },
                    { 90, "GT", "GTM", "Guatemala" },
                    { 91, "GG", "GGY", "Guernsey" },
                    { 92, "GN", "GIN", "Guinea" },
                    { 93, "GW", "GNB", "Guinea-Bissau" },
                    { 94, "GY", "GUY", "Guyana" },
                    { 95, "HT", "HTI", "Haiti" },
                    { 96, "HM", "HMD", "Heard Island and Mcdonald Islands" },
                    { 97, "HN", "HND", "Honduras" },
                    { 98, "HK", "HKG", "Hong Kong" },
                    { 99, "HU", "HUN", "Hungary" },
                    { 100, "IS", "ISL", "Iceland" },
                    { 101, "IN", "IND", "India" },
                    { 102, "ID", "IDN", "Indonesia" },
                    { 103, "IR", "IRN", "Iran" },
                    { 104, "IQ", "IRQ", "Iraq" },
                    { 105, "IE", "IRL", "Ireland" },
                    { 106, "IM", "IMN", "Isle of Man" },
                    { 107, "IL", "ISR", "Israel" },
                    { 108, "IT", "ITA", "Italy" },
                    { 109, "JM", "JAM", "Jamaica" },
                    { 110, "JP", "JPN", "Japan" },
                    { 111, "JE", "JEY", "Jersey" },
                    { 112, "JO", "JOR", "Jordan" },
                    { 113, "KZ", "KAZ", "Kazakhstan" },
                    { 114, "KE", "KEN", "Kenya" },
                    { 115, "KI", "KIR", "Kiribati" },
                    { 116, "KW", "KWT", "Kuwait" },
                    { 117, "KG", "KGZ", "Kyrgyzstan" },
                    { 118, "LA", "LAO", "Lao People's Democratic Republic" },
                    { 119, "LV", "LVA", "Latvia" },
                    { 120, "LB", "LBN", "Lebanon" },
                    { 121, "LS", "LSO", "Lesotho" },
                    { 122, "LR", "LBR", "Liberia" },
                    { 123, "LY", "LBY", "Libya" },
                    { 124, "LI", "LIE", "Liechtenstein" },
                    { 125, "LT", "LTU", "Lithuania" },
                    { 126, "LU", "LUX", "Luxembourg" },
                    { 127, "MO", "MAC", "Macao" },
                    { 128, "MK", "MKD", "Macedonia" },
                    { 129, "MG", "MDG", "Madagascar" },
                    { 130, "MW", "MWI", "Malawi" },
                    { 131, "MY", "MYS", "Malaysia" },
                    { 132, "MV", "MDV", "Maldives" },
                    { 133, "ML", "MLI", "Mali" },
                    { 134, "MT", "MLT", "Malta" },
                    { 135, "MH", "MHL", "Marshall Islands" },
                    { 136, "MQ", "MTQ", "Martinique" },
                    { 137, "MR", "MRT", "Mauritania" },
                    { 138, "MU", "MUS", "Mauritius" },
                    { 139, "YT", "MYT", "Mayotte" },
                    { 140, "MX", "MEX", "Mexico" },
                    { 141, "FM", "FSM", "Micronesia" },
                    { 142, "MD", "MDA", "Moldova" },
                    { 143, "MC", "MCO", "Monaco" },
                    { 144, "MN", "MNG", "Mongolia" },
                    { 145, "ME", "MNE", "Montenegro" },
                    { 146, "MS", "MSR", "Montserrat" },
                    { 147, "MA", "MAR", "Morocco" },
                    { 148, "MZ", "MOZ", "Mozambique" },
                    { 149, "MM", "MMR", "Myanmar" },
                    { 150, "NA", "NAM", "Namibia" },
                    { 151, "NR", "NRU", "Nauru" },
                    { 152, "NP", "NPL", "Nepal" },
                    { 153, "NL", "NLD", "Netherlands" },
                    { 154, "NC", "NCL", "New Caledonia" },
                    { 155, "NZ", "NZL", "New Zealand" },
                    { 156, "NI", "NIC", "Nicaragua" },
                    { 157, "NE", "NER", "Niger" },
                    { 158, "NG", "NGA", "Nigeria" },
                    { 159, "NU", "NIU", "Niue" },
                    { 160, "NF", "NFK", "Norfolk Island" },
                    { 161, "KP", "PRK", "North Korea" },
                    { 162, "MP", "MNP", "Northern Mariana Islands" },
                    { 163, "NO", "NOR", "Norway" },
                    { 164, "OM", "OMN", "Oman" },
                    { 165, "PK", "PAK", "Pakistan" },
                    { 166, "PW", "PLW", "Palau" },
                    { 167, "PS", "PSE", "Palestinian Territory" },
                    { 168, "PA", "PAN", "Panama" },
                    { 169, "PG", "PNG", "Papua New Guinea" },
                    { 170, "PY", "PRY", "Paraguay" },
                    { 171, "PE", "PER", "Peru" },
                    { 172, "PH", "PHL", "Philippines" },
                    { 173, "PN", "PCN", "Pitcairn" },
                    { 174, "PL", "POL", "Poland" },
                    { 175, "PT", "PRT", "Portugal" },
                    { 176, "PR", "PRI", "Puerto Rico" },
                    { 177, "QA", "QAT", "Qatar" },
                    { 178, "RO", "ROU", "Romania" },
                    { 179, "RU", "RUS", "Russia" },
                    { 180, "RW", "RWA", "Rwanda" },
                    { 181, "RE", "REU", "Réunion" },
                    { 182, "BL", "BLM", "Saint Barthélemy" },
                    { 183, "SH", "SHN", "Saint Helena, Ascension and Tristan Da Cunha" },
                    { 184, "KN", "KNA", "Saint Kitts and Nevis" },
                    { 185, "LC", "LCA", "Saint Lucia" },
                    { 186, "MF", "MAF", "Saint Martin (French Part)" },
                    { 187, "PM", "SPM", "Saint Pierre and Miquelon" },
                    { 188, "VC", "VCT", "Saint Vincent and The Grenadines" },
                    { 189, "WS", "WSM", "Samoa" },
                    { 190, "SM", "SMR", "San Marino" },
                    { 191, "ST", "STP", "Sao Tome and Principe" },
                    { 192, "SA", "SAU", "Saudi Arabia" },
                    { 193, "SN", "SEN", "Senegal" },
                    { 194, "RS", "SRB", "Serbia" },
                    { 195, "SC", "SYC", "Seychelles" },
                    { 196, "SL", "SLE", "Sierra Leone" },
                    { 197, "SG", "SGP", "Singapore" },
                    { 198, "SX", "SXM", "Sint Maarten (Dutch Part)" },
                    { 199, "SK", "SVK", "Slovakia" },
                    { 200, "SI", "SVN", "Slovenia" },
                    { 201, "SB", "SLB", "Solomon Islands" },
                    { 202, "SO", "SOM", "Somalia" },
                    { 203, "ZA", "ZAF", "South Africa" },
                    { 204, "GS", "SGS", "South Georgia" },
                    { 205, "KR", "KOR", "South Korea" },
                    { 206, "SS", "SSD", "South Sudan" },
                    { 207, "ES", "ESP", "Spain" },
                    { 208, "LK", "LKA", "Sri Lanka" },
                    { 209, "SD", "SDN", "Sudan" },
                    { 210, "SR", "SUR", "Suriname" },
                    { 211, "SJ", "SJM", "Svalbard and Jan Mayen" },
                    { 212, "SZ", "SWZ", "Swaziland" },
                    { 213, "SE", "SWE", "Sweden" },
                    { 214, "CH", "CHE", "Switzerland" },
                    { 215, "SY", "SYR", "Syrian Arab Republic" },
                    { 216, "TW", "TWN", "Taiwan" },
                    { 217, "TJ", "TJK", "Tajikistan" },
                    { 218, "TZ", "TZA", "Tanzania" },
                    { 219, "TH", "THA", "Thailand" },
                    { 220, "TL", "TLS", "Timor-Leste" },
                    { 221, "TG", "TGO", "Togo" },
                    { 222, "TK", "TKL", "Tokelau" },
                    { 223, "TO", "TON", "Tonga" },
                    { 224, "TT", "TTO", "Trinidad and Tobago" },
                    { 225, "TN", "TUN", "Tunisia" },
                    { 226, "TR", "TUR", "Turkey" },
                    { 227, "TM", "TKM", "Turkmenistan" },
                    { 228, "TC", "TCA", "Turks and Caicos Islands" },
                    { 229, "TV", "TUV", "Tuvalu" },
                    { 230, "UG", "UGA", "Uganda" },
                    { 231, "UA", "UKR", "Ukraine" },
                    { 232, "AE", "ARE", "United Arab Emirates" },
                    { 233, "GB", "GBR", "United Kingdom" },
                    { 234, "US", "USA", "United States" },
                    { 235, "UM", "UMI", "United States Minor Outlying Islands" },
                    { 236, "UY", "URY", "Uruguay" },
                    { 237, "UZ", "UZB", "Uzbekistan" },
                    { 238, "VU", "VUT", "Vanuatu" },
                    { 239, "VA", "VAT", "Vatican City" },
                    { 240, "VE", "VEN", "Venezuela" },
                    { 241, "VN", "VNM", "Viet Nam" },
                    { 242, "VG", "VGB", "Virgin Islands, British" },
                    { 243, "VI", "VIR", "Virgin Islands, U.S." },
                    { 244, "WF", "WLF", "Wallis and Futuna" },
                    { 245, "EH", "ESH", "Western Sahara" },
                    { 246, "YE", "YEM", "Yemen" },
                    { 247, "ZM", "ZMB", "Zambia" },
                    { 248, "ZW", "ZWE", "Zimbabwe" },
                    { 249, "AX", "ALA", "Åland Islands" }
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { 1, "aa", "Afar" },
                    { 2, "ab", "Abkhazian" },
                    { 3, "ae", "Avestan" },
                    { 4, "af", "Afrikaans" },
                    { 5, "ak", "Akan" },
                    { 6, "am", "Amharic" },
                    { 7, "an", "Aragonese" },
                    { 8, "ar", "Arabic" },
                    { 9, "as", "Assamese" },
                    { 10, "av", "Avaric" },
                    { 11, "ay", "Aymara" },
                    { 12, "az", "Azerbaijani" },
                    { 13, "ba", "Bashkir" },
                    { 14, "be", "Belarusian" },
                    { 15, "bg", "Bulgarian" },
                    { 16, "bh", "Bihari languages" },
                    { 17, "bi", "Bislama" },
                    { 18, "bm", "Bambara" },
                    { 19, "bn", "Bengali" },
                    { 20, "bo", "Tibetan" },
                    { 21, "br", "Breton" },
                    { 22, "bs", "Bosnian" },
                    { 23, "ca", "Catalan; Valencian" },
                    { 24, "ce", "Chechen" },
                    { 25, "ch", "Chamorro" },
                    { 26, "co", "Corsican" },
                    { 27, "cr", "Cree" },
                    { 28, "cs", "Czech" },
                    { 29, "cv", "Chuvash" },
                    { 30, "cy", "Welsh" },
                    { 31, "da", "Danish" },
                    { 32, "de", "German" },
                    { 33, "dv", "Divehi; Dhivehi; Maldivian" },
                    { 34, "dz", "Dzongkha" },
                    { 35, "ee", "Ewe" },
                    { 36, "el", "Greek, Modern (1453-)" },
                    { 37, "en", "English" },
                    { 38, "eo", "Esperanto" },
                    { 39, "es", "Spanish; Castilian" },
                    { 40, "et", "Estonian" },
                    { 41, "eu", "Basque" },
                    { 42, "fa", "Persian" },
                    { 43, "ff", "Fulah" },
                    { 44, "fi", "Finnish" },
                    { 45, "fj", "Fijian" },
                    { 46, "fo", "Faroese" },
                    { 47, "fr", "French" },
                    { 48, "fy", "Western Frisian" },
                    { 49, "ga", "Irish" },
                    { 50, "gd", "Gaelic; Scomttish Gaelic" },
                    { 51, "gl", "Galician" },
                    { 52, "gn", "Guarani" },
                    { 53, "gu", "Gujarati" },
                    { 54, "gv", "Manx" },
                    { 55, "ha", "Hausa" },
                    { 56, "he", "Hebrew" },
                    { 57, "hi", "Hindi" },
                    { 58, "ho", "Hiri Motu" },
                    { 59, "hr", "Croatian" },
                    { 60, "ht", "Haitian; Haitian Creole" },
                    { 61, "hu", "Hungarian" },
                    { 62, "hy", "Armenian" },
                    { 63, "hz", "Herero" },
                    { 64, "id", "Indonesian" },
                    { 65, "ie", "Interlingue; Occidental" },
                    { 66, "ig", "Igbo" },
                    { 67, "ii", "Sichuan Yi; Nuosu" },
                    { 68, "ik", "Inupiaq" },
                    { 69, "io", "Ido" },
                    { 70, "is", "Icelandic" },
                    { 71, "it", "Italian" },
                    { 72, "iu", "Inuktitut" },
                    { 73, "ja", "Japanese" },
                    { 74, "jv", "Javanese" },
                    { 75, "ka", "Georgian" },
                    { 76, "kg", "Kongo" },
                    { 77, "ki", "Kikuyu; Gikuyu" },
                    { 78, "kj", "Kuanyama; Kwanyama" },
                    { 79, "kk", "Kazakh" },
                    { 80, "kl", "Kalaallisut; Greenlandic" },
                    { 81, "km", "Central Khmer" },
                    { 82, "kn", "Kannada" },
                    { 83, "ko", "Korean" },
                    { 84, "kr", "Kanuri" },
                    { 85, "ks", "Kashmiri" },
                    { 86, "ku", "Kurdish" },
                    { 87, "kv", "Komi" },
                    { 88, "kw", "Cornish" },
                    { 89, "ky", "Kirghiz; Kyrgyz" },
                    { 90, "la", "Latin" },
                    { 91, "lb", "Luxembourgish; Letzeburgesch" },
                    { 92, "lg", "Ganda" },
                    { 93, "li", "Limburgan; Limburger; Limburgish" },
                    { 94, "ln", "Lingala" },
                    { 95, "lo", "Lao" },
                    { 96, "lt", "Lithuanian" },
                    { 97, "lu", "Luba-Katanga" },
                    { 98, "lv", "Latvian" },
                    { 99, "mg", "Malagasy" },
                    { 100, "mh", "Marshallese" },
                    { 101, "mi", "Maori" },
                    { 102, "mk", "Macedonian" },
                    { 103, "ml", "Malayalam" },
                    { 104, "mn", "Mongolian" },
                    { 105, "mr", "Marathi" },
                    { 106, "ms", "Malay" },
                    { 107, "mt", "Maltese" },
                    { 108, "my", "Burmese" },
                    { 109, "na", "Nauru" },
                    { 110, "nb", "Bokmål, Norwegian; Norwegian Bokmål" },
                    { 111, "nd", "Ndebele, North; North Ndebele" },
                    { 112, "ne", "Nepali" },
                    { 113, "ng", "Ndonga" },
                    { 114, "nl", "Dutch; Flemish" },
                    { 115, "nn", "Norwegian Nynorsk; Nynorsk, Norwegian" },
                    { 116, "no", "Norwegian" },
                    { 117, "nr", "Ndebele, South; South Ndebele" },
                    { 118, "nv", "Navajo; Navaho" },
                    { 119, "ny", "Chichewa; Chewa; Nyanja" },
                    { 120, "oc", "Occitan (post 1500)" },
                    { 121, "oj", "Ojibwa" },
                    { 122, "om", "Oromo" },
                    { 123, "or", "Oriya" },
                    { 124, "os", "Ossetian; Ossetic" },
                    { 125, "pa", "Panjabi; Punjabi" },
                    { 126, "pi", "Pali" },
                    { 127, "pl", "Polish" },
                    { 128, "ps", "Pushto; Pashto" },
                    { 129, "pt", "Portuguese" },
                    { 130, "qu", "Quechua" },
                    { 131, "rm", "Romansh" },
                    { 132, "rn", "Rundi" },
                    { 133, "ro", "Romanian; Moldavian; Moldovan" },
                    { 134, "ru", "Russian" },
                    { 135, "rw", "Kinyarwanda" },
                    { 136, "sa", "Sanskrit" },
                    { 137, "sc", "Sardinian" },
                    { 138, "sd", "Sindhi" },
                    { 139, "se", "Northern Sami" },
                    { 140, "sg", "Sango" },
                    { 141, "si", "Sinhala; Sinhalese" },
                    { 142, "sk", "Slovak" },
                    { 143, "sl", "Slovenian" },
                    { 144, "sm", "Samoan" },
                    { 145, "sn", "Shona" },
                    { 146, "so", "Somali" },
                    { 147, "sq", "Albanian" },
                    { 148, "sr", "Serbian" },
                    { 149, "ss", "Swati" },
                    { 150, "st", "Sotho, Southern" },
                    { 151, "su", "Sundanese" },
                    { 152, "sv", "Swedish" },
                    { 153, "sw", "Swahili" },
                    { 154, "ta", "Tamil" },
                    { 155, "te", "Telugu" },
                    { 156, "tg", "Tajik" },
                    { 157, "th", "Thai" },
                    { 158, "ti", "Tigrinya" },
                    { 159, "tk", "Turkmen" },
                    { 160, "tl", "Tagalog" },
                    { 161, "tn", "Tswana" },
                    { 162, "to", "Tonga (Tonga Islands)" },
                    { 163, "tr", "Turkish" },
                    { 164, "ts", "Tsonga" },
                    { 165, "tt", "Tatar" },
                    { 166, "tw", "Twi" },
                    { 167, "ty", "Tahitian" },
                    { 168, "ug", "Uighur; Uyghur" },
                    { 169, "uk", "Ukrainian" },
                    { 170, "ur", "Urdu" },
                    { 171, "uz", "Uzbek" },
                    { 172, "ve", "Venda" },
                    { 173, "vi", "Vietnamese" },
                    { 174, "vo", "Volapük" },
                    { 175, "wa", "Walloon" },
                    { 176, "wo", "Wolof" },
                    { 177, "xh", "Xhosa" },
                    { 178, "yi", "Yiddish" },
                    { 179, "yo", "Yoruba" },
                    { 180, "za", "Zhuang; Chuang" },
                    { 181, "zh", "Chinese" },
                    { 182, "zu", "Zulu" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "employer" },
                    { 3, "freelancer" },
                    { 4, "moderator" }
                });

            migrationBuilder.InsertData(
                table: "skills",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "Java" },
                    { 3, "Python" },
                    { 4, "JavaScript" },
                    { 5, "SQL" },
                    { 6, "AWS" },
                    { 7, "Azure" },
                    { 8, "Docker" },
                    { 9, "Kubernetes" },
                    { 10, "React" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar_img", "country_id", "created_at", "created_by", "display_name", "email", "external_provider", "external_provider_key", "modified_at", "modified_by", "password_hash", "role_id" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2026, 2, 20, 18, 32, 13, 106, DateTimeKind.Utc).AddTicks(4221), new Guid("11111111-1111-1111-1111-111111111111"), "Admin", "admin@mail.com", null, null, new DateTime(2026, 2, 20, 18, 32, 13, 106, DateTimeKind.Utc).AddTicks(4231), new Guid("11111111-1111-1111-1111-111111111111"), "15E01B6145A9E46373CCA10674CE101CB3F3FAE6D9497ADDF60AF6FD694D59C2-3278A42A79D4753EAEC4EF1047B15DFA", 1 });

            migrationBuilder.CreateIndex(
                name: "ix_bids_freelancer_id",
                table: "bids",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_bids_project_id",
                table: "bids",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_milestones_contract_id",
                table: "contract_milestones",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_milestones_created_by",
                table: "contract_milestones",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_contract_milestones_modified_by",
                table: "contract_milestones",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_contract_payments_contract_id",
                table: "contract_payments",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_payments_milestone_id",
                table: "contract_payments",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_created_by",
                table: "contracts",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_freelancer_id",
                table: "contracts",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_modified_by",
                table: "contracts",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_project_id",
                table: "contracts",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_dispute_resolutions_created_by",
                table: "dispute_resolutions",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_dispute_resolutions_dispute_id",
                table: "dispute_resolutions",
                column: "dispute_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_dispute_resolutions_modified_by",
                table: "dispute_resolutions",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_disputes_created_by",
                table: "disputes",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_disputes_modified_by",
                table: "disputes",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_employers_created_by",
                table: "employers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_employers_modified_by",
                table: "employers",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_created_by",
                table: "freelancers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_modified_by",
                table: "freelancers",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_skills_skills_id",
                table: "freelancers_skills",
                column: "skills_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_contract_id",
                table: "messages",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_created_by",
                table: "messages",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_messages_modified_by",
                table: "messages",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_messages_receiver_id",
                table: "messages",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_contract_id",
                table: "payments",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_created_by",
                table: "portfolio",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_freelancer_id",
                table: "portfolio",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_modified_by",
                table: "portfolio",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_project_milestones_created_by",
                table: "project_milestones",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_project_milestones_modified_by",
                table: "project_milestones",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_project_milestones_project_id",
                table: "project_milestones",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_created_by",
                table: "projects",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_projects_modified_by",
                table: "projects",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_projects_categories_project_id",
                table: "projects_categories",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_quotes_freelancer_id",
                table: "quotes",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_quotes_project_id",
                table: "quotes",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_contract_id",
                table: "reviews",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_created_by",
                table: "reviews",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_modified_by",
                table: "reviews",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_user_wallets_created_by",
                table: "user_wallets",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_user_wallets_modified_by",
                table: "user_wallets",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_users_country_id",
                table: "users",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_created_by",
                table: "users",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_modified_by",
                table: "users",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_languages_language_id",
                table: "users_languages",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_wallet_id",
                table: "wallet_transactions",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bids");

            migrationBuilder.DropTable(
                name: "contract_payments");

            migrationBuilder.DropTable(
                name: "dispute_resolutions");

            migrationBuilder.DropTable(
                name: "employers");

            migrationBuilder.DropTable(
                name: "freelancers_skills");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "portfolio");

            migrationBuilder.DropTable(
                name: "project_milestones");

            migrationBuilder.DropTable(
                name: "projects_categories");

            migrationBuilder.DropTable(
                name: "quotes");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "users_languages");

            migrationBuilder.DropTable(
                name: "wallet_transactions");

            migrationBuilder.DropTable(
                name: "contract_milestones");

            migrationBuilder.DropTable(
                name: "disputes");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "user_wallets");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "freelancers");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
