using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserFromFreelancerBCAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_user_id",
                table: "freelancers");

            migrationBuilder.DropIndex(
                name: "ix_freelancers_user_id",
                table: "freelancers");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "freelancers");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 4, 18, 50, 46, 246, DateTimeKind.Utc).AddTicks(9598), new DateTime(2026, 2, 4, 18, 50, 46, 246, DateTimeKind.Utc).AddTicks(9603), "FC7FBA33A7AC9F8A352BDFAAEA2FEB55BBB3DCAE4339D8A2A79F288679A6848A-78341D04C91E742D0705F7AAB279775B" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "freelancers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 3, 14, 51, 5, 751, DateTimeKind.Utc).AddTicks(2281), new DateTime(2026, 2, 3, 14, 51, 5, 751, DateTimeKind.Utc).AddTicks(2288), "73EA432F87B6CF53948DC072D78CCB601CD30F3C7D00546D1142C2A4571A4672-98C92CAE0B955542E285B668D6BC64D9" });

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_user_id",
                table: "freelancers",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_user_id",
                table: "freelancers",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
