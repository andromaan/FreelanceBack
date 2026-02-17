using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "budget_max",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "budget_min",
                table: "projects");

            migrationBuilder.AddColumn<decimal>(
                name: "budget",
                table: "projects",
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
                values: new object[] { new DateTime(2026, 2, 4, 19, 55, 31, 162, DateTimeKind.Utc).AddTicks(851), new DateTime(2026, 2, 4, 19, 55, 31, 162, DateTimeKind.Utc).AddTicks(858), "8151A75B677A43B88AE4A94614136B872BDA9DF688BC69330E30E39F9705B99C-844A51BD64C2AD086DB8F9C46F607919" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "budget",
                table: "projects");

            migrationBuilder.AddColumn<decimal>(
                name: "budget_max",
                table: "projects",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "budget_min",
                table: "projects",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 4, 18, 50, 46, 246, DateTimeKind.Utc).AddTicks(9598), new DateTime(2026, 2, 4, 18, 50, 46, 246, DateTimeKind.Utc).AddTicks(9603), "FC7FBA33A7AC9F8A352BDFAAEA2FEB55BBB3DCAE4339D8A2A79F288679A6848A-78341D04C91E742D0705F7AAB279775B" });
        }
    }
}
