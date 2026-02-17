using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFreelacerAndPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_portfolio_freelancers_freelancer_id",
                table: "portfolio");

            migrationBuilder.DropColumn(
                name: "hourly_rate",
                table: "freelancers");

            migrationBuilder.AlterColumn<Guid>(
                name: "freelancer_id",
                table: "portfolio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 14, 18, 43, 31, 223, DateTimeKind.Utc).AddTicks(9777), new DateTime(2026, 2, 14, 18, 43, 31, 223, DateTimeKind.Utc).AddTicks(9783), "D1BFB85943AA490E8E1200FEA71C1C71700B2907F8343AB11A1EB32A5DD5B9E7-64049FF963E5679AA91F78533D128734" });

            migrationBuilder.AddForeignKey(
                name: "fk_portfolio_freelancers_freelancer_id",
                table: "portfolio",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_portfolio_freelancers_freelancer_id",
                table: "portfolio");

            migrationBuilder.AlterColumn<Guid>(
                name: "freelancer_id",
                table: "portfolio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<decimal>(
                name: "hourly_rate",
                table: "freelancers",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 13, 22, 19, 41, 462, DateTimeKind.Utc).AddTicks(4139), new DateTime(2026, 2, 13, 22, 19, 41, 462, DateTimeKind.Utc).AddTicks(4146), "B83A8AA482BA35599591194CE5AD448E65DAA95AA8246E5D4AEE3624D377B38C-8A85C6C5A4DFF1ADEA2CD0319B2A5874" });

            migrationBuilder.AddForeignKey(
                name: "fk_portfolio_freelancers_freelancer_id",
                table: "portfolio",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id");
        }
    }
}
