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
                name: "employers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    company_website = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employers", x => x.id);
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
                    id = table.Column<string>(type: "text", nullable: false),
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
                    role_id = table.Column<string>(type: "text", nullable: false),
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
                name: "freelancers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bio = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    hourly_rate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    location = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    avatar_logo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freelancers", x => x.id);
                    table.ForeignKey(
                        name: "fk_freelancers_country_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_freelancers_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_freelancers_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_freelancers_users_user_id",
                        column: x => x.user_id,
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_wallets_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "freelancers_languages",
                columns: table => new
                {
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    languages_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freelancers_languages", x => new { x.freelancer_id, x.languages_id });
                    table.ForeignKey(
                        name: "fk_freelancers_languages_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_freelancers_languages_language_languages_id",
                        column: x => x.languages_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "portfolio_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    file_url = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_portfolio_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_portfolio_items_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_portfolio_items_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_portfolio_items_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contracts_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "proposals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    cover_letter = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proposals", x => x.id);
                    table.ForeignKey(
                        name: "fk_proposals_freelancers_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "freelancers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_proposals_project_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_proposals_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_proposals_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
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
                        name: "fk_messages_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stripe_payment_intent_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
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
                    table.ForeignKey(
                        name: "fk_payments_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payments_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { 1, "", "" },
                    { 2, "", "" },
                    { 3, "", "" },
                    { 4, "", "" },
                    { 5, "", "" },
                    { 6, "", "" },
                    { 7, "", "" },
                    { 8, "", "" },
                    { 9, "", "" },
                    { 10, "", "" },
                    { 11, "", "" },
                    { 12, "", "" },
                    { 13, "", "" },
                    { 14, "", "" },
                    { 15, "", "" },
                    { 16, "", "" },
                    { 17, "", "" },
                    { 18, "", "" },
                    { 19, "", "" },
                    { 20, "", "" },
                    { 21, "", "" },
                    { 22, "", "" },
                    { 23, "", "" },
                    { 24, "", "" },
                    { 25, "", "" },
                    { 26, "", "" },
                    { 27, "", "" },
                    { 28, "", "" },
                    { 29, "", "" },
                    { 30, "", "" },
                    { 31, "", "" },
                    { 32, "", "" },
                    { 33, "", "" },
                    { 34, "", "" },
                    { 35, "", "" },
                    { 36, "", "" },
                    { 37, "", "" },
                    { 38, "", "" },
                    { 39, "", "" },
                    { 40, "", "" },
                    { 41, "", "" },
                    { 42, "", "" },
                    { 43, "", "" },
                    { 44, "", "" },
                    { 45, "", "" },
                    { 46, "", "" },
                    { 47, "", "" },
                    { 48, "", "" },
                    { 49, "", "" },
                    { 50, "", "" },
                    { 51, "", "" },
                    { 52, "", "" },
                    { 53, "", "" },
                    { 54, "", "" },
                    { 55, "", "" },
                    { 56, "", "" },
                    { 57, "", "" },
                    { 58, "", "" },
                    { 59, "", "" },
                    { 60, "", "" },
                    { 61, "", "" },
                    { 62, "", "" },
                    { 63, "", "" },
                    { 64, "", "" },
                    { 65, "", "" },
                    { 66, "", "" },
                    { 67, "", "" },
                    { 68, "", "" },
                    { 69, "", "" },
                    { 70, "", "" },
                    { 71, "", "" },
                    { 72, "", "" },
                    { 73, "", "" },
                    { 74, "", "" },
                    { 75, "", "" },
                    { 76, "", "" },
                    { 77, "", "" },
                    { 78, "", "" },
                    { 79, "", "" },
                    { 80, "", "" },
                    { 81, "", "" },
                    { 82, "", "" },
                    { 83, "", "" },
                    { 84, "", "" },
                    { 85, "", "" },
                    { 86, "", "" },
                    { 87, "", "" },
                    { 88, "", "" },
                    { 89, "", "" },
                    { 90, "", "" },
                    { 91, "", "" },
                    { 92, "", "" },
                    { 93, "", "" },
                    { 94, "", "" },
                    { 95, "", "" },
                    { 96, "", "" },
                    { 97, "", "" },
                    { 98, "", "" },
                    { 99, "", "" },
                    { 100, "", "" },
                    { 101, "", "" },
                    { 102, "", "" },
                    { 103, "", "" },
                    { 104, "", "" },
                    { 105, "", "" },
                    { 106, "", "" },
                    { 107, "", "" },
                    { 108, "", "" },
                    { 109, "", "" },
                    { 110, "", "" },
                    { 111, "", "" },
                    { 112, "", "" },
                    { 113, "", "" },
                    { 114, "", "" },
                    { 115, "", "" },
                    { 116, "", "" },
                    { 117, "", "" },
                    { 118, "", "" },
                    { 119, "", "" },
                    { 120, "", "" },
                    { 121, "", "" },
                    { 122, "", "" },
                    { 123, "", "" },
                    { 124, "", "" },
                    { 125, "", "" },
                    { 126, "", "" },
                    { 127, "", "" },
                    { 128, "", "" },
                    { 129, "", "" },
                    { 130, "", "" },
                    { 131, "", "" },
                    { 132, "", "" },
                    { 133, "", "" },
                    { 134, "", "" },
                    { 135, "", "" },
                    { 136, "", "" },
                    { 137, "", "" },
                    { 138, "", "" },
                    { 139, "", "" },
                    { 140, "", "" },
                    { 141, "", "" },
                    { 142, "", "" },
                    { 143, "", "" },
                    { 144, "", "" },
                    { 145, "", "" },
                    { 146, "", "" },
                    { 147, "", "" },
                    { 148, "", "" },
                    { 149, "", "" },
                    { 150, "", "" },
                    { 151, "", "" },
                    { 152, "", "" },
                    { 153, "", "" },
                    { 154, "", "" },
                    { 155, "", "" },
                    { 156, "", "" },
                    { 157, "", "" },
                    { 158, "", "" },
                    { 159, "", "" },
                    { 160, "", "" },
                    { 161, "", "" },
                    { 162, "", "" },
                    { 163, "", "" },
                    { 164, "", "" },
                    { 165, "", "" },
                    { 166, "", "" },
                    { 167, "", "" },
                    { 168, "", "" },
                    { 169, "", "" },
                    { 170, "", "" },
                    { 171, "", "" },
                    { 172, "", "" },
                    { 173, "", "" },
                    { 174, "", "" },
                    { 175, "", "" },
                    { 176, "", "" },
                    { 177, "", "" },
                    { 178, "", "" },
                    { 179, "", "" },
                    { 180, "", "" },
                    { 181, "", "" },
                    { 182, "", "" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "admin", "admin" },
                    { "employer", "employer" },
                    { "freelancer", "freelancer" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar_img", "created_at", "created_by", "display_name", "email", "external_provider", "external_provider_key", "modified_at", "modified_by", "password_hash", "role_id" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 1, 25, 14, 16, 3, 579, DateTimeKind.Utc).AddTicks(5164), new Guid("11111111-1111-1111-1111-111111111111"), "Admin", "admin@mail.com", null, null, new DateTime(2026, 1, 25, 14, 16, 3, 579, DateTimeKind.Utc).AddTicks(5170), new Guid("11111111-1111-1111-1111-111111111111"), "A5566B40AED28E094BC69898A81686A7A946BE9B664BAED105CA5CCEF9113CCC-9EB04467721120139D0760ED1A2CDED9", "admin" });

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
                name: "ix_freelancers_country_id",
                table: "freelancers",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_created_by",
                table: "freelancers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_modified_by",
                table: "freelancers",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_user_id",
                table: "freelancers",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_languages_languages_id",
                table: "freelancers_languages",
                column: "languages_id");

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
                name: "ix_messages_sender_id",
                table: "messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_contract_id",
                table: "payments",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_created_by",
                table: "payments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_payments_modified_by",
                table: "payments",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_items_created_by",
                table: "portfolio_items",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_items_freelancer_id",
                table: "portfolio_items",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_items_modified_by",
                table: "portfolio_items",
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
                name: "ix_proposals_created_by",
                table: "proposals",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_proposals_freelancer_id",
                table: "proposals",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_proposals_modified_by",
                table: "proposals",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_proposals_project_id",
                table: "proposals",
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
                name: "ix_user_wallets_created_by",
                table: "user_wallets",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_user_wallets_modified_by",
                table: "user_wallets",
                column: "modified_by");

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
                name: "contract_milestones");

            migrationBuilder.DropTable(
                name: "employers");

            migrationBuilder.DropTable(
                name: "freelancers_languages");

            migrationBuilder.DropTable(
                name: "freelancers_skills");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "portfolio_items");

            migrationBuilder.DropTable(
                name: "project_milestones");

            migrationBuilder.DropTable(
                name: "projects_categories");

            migrationBuilder.DropTable(
                name: "proposals");

            migrationBuilder.DropTable(
                name: "quotes");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "wallet_transactions");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "user_wallets");

            migrationBuilder.DropTable(
                name: "freelancers");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
