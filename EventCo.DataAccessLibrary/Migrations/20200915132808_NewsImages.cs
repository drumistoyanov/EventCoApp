using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class NewsImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_News_NewsID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_NewsID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "NewsID",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "NewsImage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ImageData = table.Column<string>(nullable: false),
                    NewsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsImage_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsImage_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsImage_CreatedById",
                table: "NewsImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImage",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsImage");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "NewsID",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsID",
                table: "Images",
                column: "NewsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_News_NewsID",
                table: "Images",
                column: "NewsID",
                principalTable: "News",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
