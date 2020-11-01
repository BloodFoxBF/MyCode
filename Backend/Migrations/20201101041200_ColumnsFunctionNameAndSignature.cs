using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class ColumnsFunctionNameAndSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FunctionName",
                table: "Problems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionSignature",
                table: "Problems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionName",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "FunctionSignature",
                table: "Problems");
        }
    }
}
