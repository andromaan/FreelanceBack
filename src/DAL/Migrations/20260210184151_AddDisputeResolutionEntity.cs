using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDisputeResolutionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 10, 18, 41, 51, 186, DateTimeKind.Utc).AddTicks(2046), new DateTime(2026, 2, 10, 18, 41, 51, 186, DateTimeKind.Utc).AddTicks(2054), "1768195AA75D2F4FA164A00BFD0F4407AC66321BD14851E3C6CB48B7019C926F-2030E9A7D90CD32DEF4AC91591A61469" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 10, 17, 13, 58, 758, DateTimeKind.Utc).AddTicks(1949), new DateTime(2026, 2, 10, 17, 13, 58, 758, DateTimeKind.Utc).AddTicks(1955), "3E740215BAB6EEA9D1BC3DA77B3051B1B1BE74FFBF15627E18247A287C467236-B6F1027CE9E418B27965B1A49C1D0544" });
        }
    }
}
