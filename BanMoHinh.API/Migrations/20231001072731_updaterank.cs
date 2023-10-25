using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanMoHinh.API.Migrations
{
    public partial class updaterank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoinsMax",
                table: "Rank",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointsMin",
                table: "Rank",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoinsMax",
                table: "Rank");

            migrationBuilder.DropColumn(
                name: "PointsMin",
                table: "Rank");
        }
    }
}
