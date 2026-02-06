using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reviewed_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false),
                    review_text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    reviewer_role_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 6, 11, 10, 21, 62, DateTimeKind.Utc).AddTicks(4189), new DateTime(2026, 2, 6, 11, 10, 21, 62, DateTimeKind.Utc).AddTicks(4195), "D50F11A364F66163C6F370317BA4763E15C9811386A1759E4A79AE53ECC9133E-125251D3454087D4C09EB4F0731A84E6" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 4, 19, 55, 31, 162, DateTimeKind.Utc).AddTicks(851), new DateTime(2026, 2, 4, 19, 55, 31, 162, DateTimeKind.Utc).AddTicks(858), "8151A75B677A43B88AE4A94614136B872BDA9DF688BC69330E30E39F9705B99C-844A51BD64C2AD086DB8F9C46F607919" });
        }
    }
}
