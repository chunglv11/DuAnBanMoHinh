using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanMoHinh.API.Migrations
{
    public partial class mgss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_VoucherType_VoucherTypeId",
                table: "Voucher");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherTypeId",
                table: "Voucher",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "VoucherStatusId",
                table: "Voucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VoucherStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_VoucherStatusId",
                table: "Voucher",
                column: "VoucherStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_VoucherStatus_VoucherStatusId",
                table: "Voucher",
                column: "VoucherStatusId",
                principalTable: "VoucherStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_VoucherType_VoucherTypeId",
                table: "Voucher",
                column: "VoucherTypeId",
                principalTable: "VoucherType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_VoucherStatus_VoucherStatusId",
                table: "Voucher");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_VoucherType_VoucherTypeId",
                table: "Voucher");

            migrationBuilder.DropTable(
                name: "VoucherStatus");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_VoucherStatusId",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "VoucherStatusId",
                table: "Voucher");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherTypeId",
                table: "Voucher",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_VoucherType_VoucherTypeId",
                table: "Voucher",
                column: "VoucherTypeId",
                principalTable: "VoucherType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
