using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMessagesTableForNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_sender_id",
                table: "messages");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "messages",
                newName: "receiver_id");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "messages",
                newName: "text");

            migrationBuilder.RenameIndex(
                name: "ix_messages_sender_id",
                table: "messages",
                newName: "ix_messages_receiver_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "contract_id",
                table: "messages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 2, 19, 3, 37, 121, DateTimeKind.Utc).AddTicks(6251), new DateTime(2026, 2, 2, 19, 3, 37, 121, DateTimeKind.Utc).AddTicks(6257), "A4D6703E27FDBB8ABEBCA136B20EDE59FCAD77AC5A7752A0DB6FFA16A2F2589C-A7A0E34B2B71FAD99F3BC19C5505FD06" });

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_receiver_id",
                table: "messages",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_receiver_id",
                table: "messages");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "messages",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "receiver_id",
                table: "messages",
                newName: "sender_id");

            migrationBuilder.RenameIndex(
                name: "ix_messages_receiver_id",
                table: "messages",
                newName: "ix_messages_sender_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "contract_id",
                table: "messages",
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
                values: new object[] { new DateTime(2026, 2, 2, 15, 52, 12, 376, DateTimeKind.Utc).AddTicks(9582), new DateTime(2026, 2, 2, 15, 52, 12, 376, DateTimeKind.Utc).AddTicks(9589), "769C4C0D4C9C5517F895B075F3209FCDF28297C9AA2A3CB884294946361A3A4C-921F4AF4A871E6C9402322210A948815" });

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_sender_id",
                table: "messages",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
