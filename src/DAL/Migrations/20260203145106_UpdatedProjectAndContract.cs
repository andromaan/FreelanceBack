using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProjectAndContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_hourly",
                table: "projects");

            migrationBuilder.AddColumn<DateTime>(
                name: "deadline",
                table: "projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 3, 14, 51, 5, 751, DateTimeKind.Utc).AddTicks(2281), new DateTime(2026, 2, 3, 14, 51, 5, 751, DateTimeKind.Utc).AddTicks(2288), "73EA432F87B6CF53948DC072D78CCB601CD30F3C7D00546D1142C2A4571A4672-98C92CAE0B955542E285B668D6BC64D9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deadline",
                table: "projects");

            migrationBuilder.AddColumn<bool>(
                name: "is_hourly",
                table: "projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 2, 19, 3, 37, 121, DateTimeKind.Utc).AddTicks(6251), new DateTime(2026, 2, 2, 19, 3, 37, 121, DateTimeKind.Utc).AddTicks(6257), "A4D6703E27FDBB8ABEBCA136B20EDE59FCAD77AC5A7752A0DB6FFA16A2F2589C-A7A0E34B2B71FAD99F3BC19C5505FD06" });
        }
    }
}
