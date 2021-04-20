using Microsoft.EntityFrameworkCore.Migrations;

namespace Yangtze.DAL.Migrations
{
    public partial class User_IsActive_property_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
