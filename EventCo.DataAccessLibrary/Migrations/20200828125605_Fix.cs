using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_EventId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatedById",
                table: "Messages",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_CreatedById",
                table: "Messages",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_CreatedById",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_CreatedById",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_EventId",
                table: "Messages",
                column: "EventId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
