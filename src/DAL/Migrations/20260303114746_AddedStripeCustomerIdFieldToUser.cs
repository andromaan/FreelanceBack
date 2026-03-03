using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedStripeCustomerIdFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "stripe_customer_id",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash", "stripe_customer_id" },
                values: new object[] { new DateTime(2026, 3, 3, 11, 47, 45, 436, DateTimeKind.Utc).AddTicks(4460), new DateTime(2026, 3, 3, 11, 47, 45, 436, DateTimeKind.Utc).AddTicks(4466), "885C7FB0AAB332EA00C5CE8B25CC9AF52CC3FE74BC1995AEE2628F04A442277D-4FFDC4EA29B9A0A2AEAB8EF9B43EFFC4", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stripe_customer_id",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 23, 16, 3, 41, 551, DateTimeKind.Utc).AddTicks(1541), new DateTime(2026, 2, 23, 16, 3, 41, 551, DateTimeKind.Utc).AddTicks(1546), "7B0171A7490C1BB725B169EBF09915C3B5F15F80FD97F5E3C1C690ED2057D261-B4662EFE42765101A1A649297F3CD73D" });
        }
    }
}
