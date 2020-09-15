using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCoApp.DataAccessLibrary.Migrations
{
    public partial class ModifiedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_ModifiedById",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_ModifiedById",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_ModifiedById",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ModifiedById",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_ModifiedById",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ModifiedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ModifiedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_News_ModifiedById",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ModifiedById",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Logs_ModifiedById",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Images_ModifiedById",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ModifiedById",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Bookings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "News",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "News",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ModifiedById",
                table: "Tickets",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_News_ModifiedById",
                table: "News",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ModifiedById",
                table: "Messages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ModifiedById",
                table: "Logs",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ModifiedById",
                table: "Images",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ModifiedById",
                table: "Bookings",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_ModifiedById",
                table: "Bookings",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_ModifiedById",
                table: "Images",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_ModifiedById",
                table: "Logs",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ModifiedById",
                table: "Messages",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_ModifiedById",
                table: "News",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ModifiedById",
                table: "Tickets",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
