using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy_Spice_MVC.Data.Migrations
{
    public partial class AddOrderHeaderAndDetailsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_headers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(nullable: false),
                    order_date = table.Column<DateTime>(nullable: false),
                    order_total_original = table.Column<double>(nullable: false),
                    order_total = table.Column<double>(nullable: false),
                    pickup_time = table.Column<DateTime>(nullable: false),
                    coupon_code = table.Column<string>(nullable: true),
                    coupon_code_discount = table.Column<double>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    payment_status = table.Column<string>(nullable: true),
                    comments = table.Column<string>(nullable: true),
                    pickup_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    transaction_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_headers", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_headers_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(nullable: false),
                    menu_item_id = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_details_menu_items_menu_item_id",
                        column: x => x.menu_item_id,
                        principalTable: "menu_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_details_order_headers_order_id",
                        column: x => x.order_id,
                        principalTable: "order_headers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_details_menu_item_id",
                table: "order_details",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_order_id",
                table: "order_details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_headers_user_id",
                table: "order_headers",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "order_headers");
        }
    }
}
