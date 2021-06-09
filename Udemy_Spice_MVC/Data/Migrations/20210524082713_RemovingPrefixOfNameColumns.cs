using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy_Spice_MVC.Data.Migrations
{
    public partial class RemovingPrefixOfNameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subcategory_name",
                table: "subcategories");

            migrationBuilder.DropColumn(
                name: "category_name",
                table: "categories");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "subcategories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "categories",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "subcategories");

            migrationBuilder.DropColumn(
                name: "name",
                table: "categories");

            migrationBuilder.AddColumn<string>(
                name: "subcategory_name",
                table: "subcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "category_name",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
