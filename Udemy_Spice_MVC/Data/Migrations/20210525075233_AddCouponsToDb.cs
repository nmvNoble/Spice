using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy_Spice_MVC.Data.Migrations
{
    public partial class AddCouponsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coupons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    coupon_type = table.Column<string>(nullable: false),
                    discount = table.Column<double>(nullable: false),
                    minimum_amount = table.Column<double>(nullable: false),
                    picture = table.Column<byte[]>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coupons", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coupons");
        }
    }
}
