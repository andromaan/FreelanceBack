using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureAuditUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_created_by",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_modified_by",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_user_wallets_users_created_by",
                table: "user_wallets");

            migrationBuilder.DropForeignKey(
                name: "fk_user_wallets_users_modified_by",
                table: "user_wallets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at",
                table: "employers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "employers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 54, 10, 739, DateTimeKind.Utc).AddTicks(7867), new DateTime(2026, 2, 13, 12, 54, 10, 739, DateTimeKind.Utc).AddTicks(7874), "A6F8D5218EEFF5038A54E6DEA1750C56C5F136B91444EADE1516C23C698E5D42-114A9B5DE66AC059C773A7E7673B1D88" });

            migrationBuilder.CreateIndex(
                name: "ix_employers_created_by",
                table: "employers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_employers_modified_by",
                table: "employers",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_employers_users_created_by",
                table: "employers",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employers_users_modified_by",
                table: "employers",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_created_by",
                table: "freelancers",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_modified_by",
                table: "freelancers",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_wallets_users_created_by",
                table: "user_wallets",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_wallets_users_modified_by",
                table: "user_wallets",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employers_users_created_by",
                table: "employers");

            migrationBuilder.DropForeignKey(
                name: "fk_employers_users_modified_by",
                table: "employers");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_created_by",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_users_modified_by",
                table: "freelancers");

            migrationBuilder.DropForeignKey(
                name: "fk_user_wallets_users_created_by",
                table: "user_wallets");

            migrationBuilder.DropForeignKey(
                name: "fk_user_wallets_users_modified_by",
                table: "user_wallets");

            migrationBuilder.DropIndex(
                name: "ix_employers_created_by",
                table: "employers");

            migrationBuilder.DropIndex(
                name: "ix_employers_modified_by",
                table: "employers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at",
                table: "employers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "timezone('utc', now())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "employers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "timezone('utc', now())");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 10, 18, 41, 51, 186, DateTimeKind.Utc).AddTicks(2046), new DateTime(2026, 2, 10, 18, 41, 51, 186, DateTimeKind.Utc).AddTicks(2054), "1768195AA75D2F4FA164A00BFD0F4407AC66321BD14851E3C6CB48B7019C926F-2030E9A7D90CD32DEF4AC91591A61469" });

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_created_by",
                table: "freelancers",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_users_modified_by",
                table: "freelancers",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_user_wallets_users_created_by",
                table: "user_wallets",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_user_wallets_users_modified_by",
                table: "user_wallets",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
