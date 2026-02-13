using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_freelancers_freelancer_id",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_contracts_users_created_by",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_contracts_users_modified_by",
                table: "contracts");

            migrationBuilder.DropTable(
                name: "portfolio_items");

            migrationBuilder.CreateTable(
                name: "portfolio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    portfolio_url = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: true),
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
                        principalColumn: "id");
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

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 13, 16, 13, 2, 328, DateTimeKind.Utc).AddTicks(1342), new DateTime(2026, 2, 13, 16, 13, 2, 328, DateTimeKind.Utc).AddTicks(1349), "0F286F4001FFC10AC7B7478C6B83A8B59C8EE6D77260A79E84F80CEA1B5BECCE-1157C0686C32D0B61A9483A2AFCE1078" });

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

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_freelancers_freelancer_id",
                table: "contracts",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_users_created_by",
                table: "contracts",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_users_modified_by",
                table: "contracts",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_freelancers_freelancer_id",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_contracts_users_created_by",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "fk_contracts_users_modified_by",
                table: "contracts");

            migrationBuilder.DropTable(
                name: "portfolio");

            migrationBuilder.CreateTable(
                name: "portfolio_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    freelancer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    file_url = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
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

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 54, 10, 739, DateTimeKind.Utc).AddTicks(7867), new DateTime(2026, 2, 13, 12, 54, 10, 739, DateTimeKind.Utc).AddTicks(7874), "A6F8D5218EEFF5038A54E6DEA1750C56C5F136B91444EADE1516C23C698E5D42-114A9B5DE66AC059C773A7E7673B1D88" });

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

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_freelancers_freelancer_id",
                table: "contracts",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_users_created_by",
                table: "contracts",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_users_modified_by",
                table: "contracts",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
