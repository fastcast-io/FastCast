using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCast.Migrations
{
    public partial class LatandLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Session",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Session",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Session");
        }
    }
}
