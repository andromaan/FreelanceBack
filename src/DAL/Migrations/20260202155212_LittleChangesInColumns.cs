using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class LittleChangesInColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "contracts");

            migrationBuilder.AddColumn<decimal>(
                name: "agreed_rate",
                table: "contracts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 2, 15, 52, 12, 376, DateTimeKind.Utc).AddTicks(9582), new DateTime(2026, 2, 2, 15, 52, 12, 376, DateTimeKind.Utc).AddTicks(9589), "769C4C0D4C9C5517F895B075F3209FCDF28297C9AA2A3CB884294946361A3A4C-921F4AF4A871E6C9402322210A948815" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "agreed_rate",
                table: "contracts");

            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "contracts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 27, 11, 27, 1, 284, DateTimeKind.Utc).AddTicks(3447), new DateTime(2026, 1, 27, 11, 27, 1, 284, DateTimeKind.Utc).AddTicks(3453), "C0509A2331C5183D05D8757EB93DE9570CDB62521C7A8E77A2E97A9DBF3026F8-503791C5E542F3ECB8786C93D5785361" });
        }
    }
}
