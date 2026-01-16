using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
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
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_language", x => x.id);
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
                name: "skill",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skill", x => x.id);
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
                name: "freelancers_info",
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
                    table.PrimaryKey("pk_freelancers_info", x => x.id);
                    table.ForeignKey(
                        name: "fk_freelancers_info_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_freelancers_info_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_freelancers_info_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_freelancers_info_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "job",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_job", x => x.id);
                    table.ForeignKey(
                        name: "fk_job_users_client_id",
                        column: x => x.client_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_job_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_job_users_modified_by",
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
                name: "freelancer_info_language",
                columns: table => new
                {
                    freelancer_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    languages_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freelancer_info_language", x => new { x.freelancer_info_id, x.languages_id });
                    table.ForeignKey(
                        name: "fk_freelancer_info_language_freelancers_info_freelancer_info_id",
                        column: x => x.freelancer_info_id,
                        principalTable: "freelancers_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_freelancer_info_language_language_languages_id",
                        column: x => x.languages_id,
                        principalTable: "language",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "portfolio_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_info_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_portfolio_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_portfolio_item_freelancers_info_freelancer_info_id",
                        column: x => x.freelancer_info_id,
                        principalTable: "freelancers_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_portfolio_item_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_portfolio_item_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_skill",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_skill", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_skill_freelancers_info_freelancer_info_id",
                        column: x => x.freelancer_info_id,
                        principalTable: "freelancers_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_skill_skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_job_job_id",
                        column: x => x.job_id,
                        principalTable: "job",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contract_users_client_id",
                        column: x => x.client_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contract_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contract_users_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contract_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "proposal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_proposal", x => x.id);
                    table.ForeignKey(
                        name: "fk_proposal_job_job_id",
                        column: x => x.job_id,
                        principalTable: "job",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_proposal_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_proposal_users_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_proposal_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "message",
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
                    table.PrimaryKey("pk_message", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_contract_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_message_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_message_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payment",
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
                    table.PrimaryKey("pk_payment", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_contract_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payment_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payment_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "country",
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
                table: "language",
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
                    { "admin", "admin" },
                    { "client", "client" },
                    { "freelancer", "freelancer" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_contract_client_id",
                table: "contract",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_created_by",
                table: "contract",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_contract_freelancer_id",
                table: "contract",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_job_id",
                table: "contract",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_modified_by",
                table: "contract",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancer_info_language_languages_id",
                table: "freelancer_info_language",
                column: "languages_id");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_info_country_id",
                table: "freelancers_info",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_info_created_by",
                table: "freelancers_info",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_info_modified_by",
                table: "freelancers_info",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_info_user_id",
                table: "freelancers_info",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_job_client_id",
                table: "job",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_job_created_by",
                table: "job",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_job_modified_by",
                table: "job",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_message_contract_id",
                table: "message",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_created_by",
                table: "message",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_message_modified_by",
                table: "message",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_message_sender_id",
                table: "message",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_contract_id",
                table: "payment",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_created_by",
                table: "payment",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_payment_modified_by",
                table: "payment",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_item_created_by",
                table: "portfolio_item",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_item_freelancer_info_id",
                table: "portfolio_item",
                column: "freelancer_info_id");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_item_modified_by",
                table: "portfolio_item",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_proposal_created_by",
                table: "proposal",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_proposal_freelancer_id",
                table: "proposal",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "ix_proposal_job_id",
                table: "proposal",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "ix_proposal_modified_by",
                table: "proposal",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_skill_freelancer_info_id",
                table: "user_skill",
                column: "freelancer_info_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_skill_skill_id",
                table: "user_skill",
                column: "skill_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "freelancer_info_language");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "portfolio_item");

            migrationBuilder.DropTable(
                name: "proposal");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "user_skill");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "freelancers_info");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "job");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
