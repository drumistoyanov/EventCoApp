using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class NewsImagesNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_CreatedById",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Events_EventId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_Users_CreatedById",
                table: "NewsImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_News_NewsId",
                table: "NewsImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsImage",
                table: "NewsImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "NewsImage",
                newName: "NewsImages");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "EventImage");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImages",
                newName: "IX_NewsImages_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImage_CreatedById",
                table: "NewsImages",
                newName: "IX_NewsImages_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Images_EventId",
                table: "EventImage",
                newName: "IX_EventImage_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CreatedById",
                table: "EventImage",
                newName: "IX_EventImage_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsImages",
                table: "NewsImages",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventImage",
                table: "EventImage",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImage_Users_CreatedById",
                table: "EventImage",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventImage_Events_EventId",
                table: "EventImage",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImages_Users_CreatedById",
                table: "NewsImages",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImages_News_NewsId",
                table: "NewsImages",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventImage_Users_CreatedById",
                table: "EventImage");

            migrationBuilder.DropForeignKey(
                name: "FK_EventImage_Events_EventId",
                table: "EventImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImages_Users_CreatedById",
                table: "NewsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImages_News_NewsId",
                table: "NewsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsImages",
                table: "NewsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventImage",
                table: "EventImage");

            migrationBuilder.RenameTable(
                name: "NewsImages",
                newName: "NewsImage");

            migrationBuilder.RenameTable(
                name: "EventImage",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImages_NewsId",
                table: "NewsImage",
                newName: "IX_NewsImage_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImages_CreatedById",
                table: "NewsImage",
                newName: "IX_NewsImage_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EventImage_EventId",
                table: "Images",
                newName: "IX_Images_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventImage_CreatedById",
                table: "Images",
                newName: "IX_Images_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsImage",
                table: "NewsImage",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_CreatedById",
                table: "Images",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Events_EventId",
                table: "Images",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_Users_CreatedById",
                table: "NewsImage",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_News_NewsId",
                table: "NewsImage",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
