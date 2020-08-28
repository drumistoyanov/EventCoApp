using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class addedcountrtoevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "Events");
        }
    }
}
