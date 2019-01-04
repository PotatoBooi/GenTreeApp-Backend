using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenTreeApp.API.Persistence.Migrations
{
    public partial class TreeNameRelationComp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relations",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Relations_PersonId",
                table: "Relations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Trees",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "SecondPersonId",
                table: "Relations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Relations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relations",
                table: "Relations",
                columns: new[] { "PersonId", "SecondPersonId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relations",
                table: "Relations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Trees");

            migrationBuilder.AlterColumn<Guid>(
                name: "SecondPersonId",
                table: "Relations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Relations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relations",
                table: "Relations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_PersonId",
                table: "Relations",
                column: "PersonId");
        }
    }
}
