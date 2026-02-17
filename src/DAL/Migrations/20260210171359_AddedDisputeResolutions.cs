using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedDisputeResolutions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 10, 17, 13, 58, 758, DateTimeKind.Utc).AddTicks(1949), new DateTime(2026, 2, 10, 17, 13, 58, 758, DateTimeKind.Utc).AddTicks(1955), "3E740215BAB6EEA9D1BC3DA77B3051B1B1BE74FFBF15627E18247A287C467236-B6F1027CE9E418B27965B1A49C1D0544" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dispute_resolutions");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 10, 15, 34, 8, 295, DateTimeKind.Utc).AddTicks(9820), new DateTime(2026, 2, 10, 15, 34, 8, 295, DateTimeKind.Utc).AddTicks(9836), "0C2575F8D923100A8FD76ED38BC79C79DE118C60E5229CDD7D85BF882D1BD369-42864329913A9846E32B44EE6BF5FC74" });
        }
    }
}
