using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class Loogereventiduserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Logs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Logs");
        }
    }
}
