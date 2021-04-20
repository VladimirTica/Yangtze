using Microsoft.EntityFrameworkCore.Migrations;

namespace Yangtze.DAL.Migrations
{
    public partial class User_Status_def_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>("Status", "User", nullable: true, defaultValueSql: "1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>("Status", "User", nullable: true, defaultValueSql: "0");
        }
    }
}
