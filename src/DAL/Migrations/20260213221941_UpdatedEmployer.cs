using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEmployer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_receiver_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_contracts_contract_id",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "employers");

            migrationBuilder.InsertData(
                table: "skills",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "Java" },
                    { 3, "Python" },
                    { 4, "JavaScript" },
                    { 5, "SQL" },
                    { 6, "AWS" },
                    { 7, "Azure" },
                    { 8, "Docker" },
                    { 9, "Kubernetes" },
                    { 10, "React" }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 13, 22, 19, 41, 462, DateTimeKind.Utc).AddTicks(4139), new DateTime(2026, 2, 13, 22, 19, 41, 462, DateTimeKind.Utc).AddTicks(4146), "B83A8AA482BA35599591194CE5AD448E65DAA95AA8246E5D4AEE3624D377B38C-8A85C6C5A4DFF1ADEA2CD0319B2A5874" });

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_receiver_id",
                table: "messages",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_contracts_contract_id",
                table: "reviews",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_receiver_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_contracts_contract_id",
                table: "reviews");

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "employers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 13, 16, 13, 2, 328, DateTimeKind.Utc).AddTicks(1342), new DateTime(2026, 2, 13, 16, 13, 2, 328, DateTimeKind.Utc).AddTicks(1349), "0F286F4001FFC10AC7B7478C6B83A8B59C8EE6D77260A79E84F80CEA1B5BECCE-1157C0686C32D0B61A9483A2AFCE1078" });

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_receiver_id",
                table: "messages",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_contracts_contract_id",
                table: "reviews",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
