using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy_Spice_MVC.Data.Migrations
{
    public partial class RenameMenu_itemToMenu_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menu_item_categories_category_id",
                table: "menu_item");

            migrationBuilder.DropForeignKey(
                name: "FK_menu_item_subcategories_subcategory_id",
                table: "menu_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_menu_item",
                table: "menu_item");

            migrationBuilder.RenameTable(
                name: "menu_item",
                newName: "menu_items");

            migrationBuilder.RenameIndex(
                name: "IX_menu_item_subcategory_id",
                table: "menu_items",
                newName: "IX_menu_items_subcategory_id");

            migrationBuilder.RenameIndex(
                name: "IX_menu_item_category_id",
                table: "menu_items",
                newName: "IX_menu_items_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_menu_items",
                table: "menu_items",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_menu_items_categories_category_id",
                table: "menu_items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_items_subcategories_subcategory_id",
                table: "menu_items",
                column: "subcategory_id",
                principalTable: "subcategories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menu_items_categories_category_id",
                table: "menu_items");

            migrationBuilder.DropForeignKey(
                name: "FK_menu_items_subcategories_subcategory_id",
                table: "menu_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_menu_items",
                table: "menu_items");

            migrationBuilder.RenameTable(
                name: "menu_items",
                newName: "menu_item");

            migrationBuilder.RenameIndex(
                name: "IX_menu_items_subcategory_id",
                table: "menu_item",
                newName: "IX_menu_item_subcategory_id");

            migrationBuilder.RenameIndex(
                name: "IX_menu_items_category_id",
                table: "menu_item",
                newName: "IX_menu_item_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_menu_item",
                table: "menu_item",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_menu_item_categories_category_id",
                table: "menu_item",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_item_subcategories_subcategory_id",
                table: "menu_item",
                column: "subcategory_id",
                principalTable: "subcategories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
