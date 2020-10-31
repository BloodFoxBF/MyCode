using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Example",
                table: "Problems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Legend",
                table: "Problems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirement",
                table: "Problems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Example",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "Legend",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "Problems");
        }
    }
}
