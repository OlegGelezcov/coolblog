using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogModel.Migrations
{
    public partial class UserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Users");
        }
    }
}
