using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class mgss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuTichDiem_QuyDoiDiem_IDQuyDoiDiem",
                table: "LichSuTichDiem");

            migrationBuilder.DropTable(
                name: "QuyDoiDiem");

            migrationBuilder.DropIndex(
                name: "IX_LichSuTichDiem_IDQuyDoiDiem",
                table: "LichSuTichDiem");

            migrationBuilder.DropColumn(
                name: "Diem",
                table: "LichSuTichDiem");

            migrationBuilder.DropColumn(
                name: "IDQuyDoiDiem",
                table: "LichSuTichDiem");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "LichSuTichDiem");

            migrationBuilder.DropColumn(
                name: "TienChiTieu",
                table: "KhachHang");

            migrationBuilder.UpdateData(
                table: "XepHang",
                keyColumn: "Id",
                keyValue: new Guid("491abc2c-3bfa-47dd-a55c-ed065295374c"),
                column: "Ten",
                value: "Đồng");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Diem",
                table: "LichSuTichDiem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "IDQuyDoiDiem",
                table: "LichSuTichDiem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "LichSuTichDiem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TienChiTieu",
                table: "KhachHang",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuyDoiDiem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TiLeTichDiem = table.Column<int>(type: "int", nullable: false),
                    TiLeTieuDiem = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyDoiDiem", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "QuyDoiDiem",
                columns: new[] { "ID", "TiLeTichDiem", "TiLeTieuDiem", "TrangThai" },
                values: new object[] { new Guid("16bd37c4-cef0-4e92-9bb5-511c43d99037"), 0, 0, 1 });

            migrationBuilder.UpdateData(
                table: "XepHang",
                keyColumn: "Id",
                keyValue: new Guid("491abc2c-3bfa-47dd-a55c-ed065295374c"),
                column: "Ten",
                value: "Thành viên");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuTichDiem_IDQuyDoiDiem",
                table: "LichSuTichDiem",
                column: "IDQuyDoiDiem");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuTichDiem_QuyDoiDiem_IDQuyDoiDiem",
                table: "LichSuTichDiem",
                column: "IDQuyDoiDiem",
                principalTable: "QuyDoiDiem",
                principalColumn: "ID");
        }
    }
}
