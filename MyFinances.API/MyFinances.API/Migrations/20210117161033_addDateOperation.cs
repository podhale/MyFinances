using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinances.API.Migrations
{
    public partial class addDateOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOperation",
                table: "Operations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOperation",
                table: "Operations");
        }
    }
}
