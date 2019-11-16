using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCast.Migrations
{
    public partial class RadiusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Radius",
                table: "Session",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Session");
        }
    }
}
