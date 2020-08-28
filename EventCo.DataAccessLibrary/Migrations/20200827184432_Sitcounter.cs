using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class Sitcounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiteCounter",
                table: "Logs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteCounter",
                table: "Logs");
        }
    }
}
