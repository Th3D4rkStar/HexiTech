using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HexiTech.Migrations
{
    public partial class AddedDateCreatedToUserOrdersList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "UserOrdersLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "UserOrdersLists");
        }
    }
}
