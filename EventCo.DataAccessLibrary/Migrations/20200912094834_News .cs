using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class News : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "News",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Counter",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "News");
        }
    }
}
