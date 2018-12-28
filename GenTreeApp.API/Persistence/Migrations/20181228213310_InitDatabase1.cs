using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenTreeApp.API.Persistence.Migrations
{
    public partial class InitDatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PersonId",
                table: "Comments",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Persons_PersonId",
                table: "Comments",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Persons_PersonId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PersonId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Comments");
        }
    }
}
