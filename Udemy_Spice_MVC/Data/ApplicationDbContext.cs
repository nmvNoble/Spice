using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Udemy_Spice_MVC.Models;//added

namespace Udemy_Spice_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<SubCategory> subcategories { get; set; }
        public DbSet<MenuItem> menu_items { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<ApplicationUser> app_users { get; set; }
        public DbSet<ShoppingCart> shopping_carts { get; set; }
        public DbSet<OrderHeader> order_headers { get; set; }
        public DbSet<OrderDetails> order_details { get; set; }
    }
}
