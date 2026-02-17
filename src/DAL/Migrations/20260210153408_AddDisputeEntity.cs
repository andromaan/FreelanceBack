using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDisputeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payments_users_created_by",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "fk_payments_users_modified_by",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "ix_payments_created_by",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "ix_payments_modified_by",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "stripe_payment_intent_id",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "payments",
                newName: "payment_method");

            migrationBuilder.RenameColumn(
                name: "modified_at",
                table: "payments",
                newName: "payment_date");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "payments",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateTable(
                name: "contract_payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    milestone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    payment_method = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_payments_contract_milestones_milestone_id",
                        column: x => x.milestone_id,
                        principalTable: "contract_milestones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contract_payments_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "disputes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disputes", x => x.id);
                    table.ForeignKey(
                        name: "fk_disputes_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_disputes_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[] { "moderator", "moderator" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 10, 15, 34, 8, 295, DateTimeKind.Utc).AddTicks(9820), new DateTime(2026, 2, 10, 15, 34, 8, 295, DateTimeKind.Utc).AddTicks(9836), "0C2575F8D923100A8FD76ED38BC79C79DE118C60E5229CDD7D85BF882D1BD369-42864329913A9846E32B44EE6BF5FC74" });

            migrationBuilder.CreateIndex(
                name: "ix_contract_payments_contract_id",
                table: "contract_payments",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_payments_milestone_id",
                table: "contract_payments",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "ix_disputes_created_by",
                table: "disputes",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_disputes_modified_by",
                table: "disputes",
                column: "modified_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contract_payments");

            migrationBuilder.DropTable(
                name: "disputes");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "moderator");

            migrationBuilder.RenameColumn(
                name: "payment_method",
                table: "payments",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "payment_date",
                table: "payments",
                newName: "modified_at");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "payments",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "payments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "payments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modified_by",
                table: "payments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "stripe_payment_intent_id",
                table: "payments",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 2, 6, 11, 10, 21, 62, DateTimeKind.Utc).AddTicks(4189), new DateTime(2026, 2, 6, 11, 10, 21, 62, DateTimeKind.Utc).AddTicks(4195), "D50F11A364F66163C6F370317BA4763E15C9811386A1759E4A79AE53ECC9133E-125251D3454087D4C09EB4F0731A84E6" });

            migrationBuilder.CreateIndex(
                name: "ix_payments_created_by",
                table: "payments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_payments_modified_by",
                table: "payments",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_payments_users_created_by",
                table: "payments",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_payments_users_modified_by",
                table: "payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
