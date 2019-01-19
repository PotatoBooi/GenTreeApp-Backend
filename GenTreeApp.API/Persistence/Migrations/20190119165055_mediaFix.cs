using Microsoft.EntityFrameworkCore.Migrations;

namespace GenTreeApp.API.Persistence.Migrations
{
    public partial class mediaFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Media");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Media",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Media");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Media",
                nullable: true);
        }
    }
}
