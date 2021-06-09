using Microsoft.AspNetCore.Authorization;//added
using Microsoft.AspNetCore.Http;//added
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//added
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;//added
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added
using Udemy_Spice_MVC.Models;//added
using Udemy_Spice_MVC.Models.ViewModels;//added
using Udemy_Spice_MVC.Utility;//added

namespace Udemy_Spice_MVC.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                menuItem = await _db.menu_items.Include(m => m.category)
                                               .Include(m => m.subcategory)
                                               .ToListAsync(),
                category = await _db.categories.ToListAsync(),
                coupon = await _db.coupons.Where(c => c.is_active == true).ToListAsync()
            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                var cnt = _db.shopping_carts.Where(u => u.app_user_id == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);
            }

            return View(IndexVM);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var menuItemFromDb = await _db.menu_items
                                          .Include(m => m.category)
                                          .Include(m => m.subcategory)
                                          .Where(m => m.id == id)
                                          .FirstOrDefaultAsync();

            ShoppingCart cartObj = new ShoppingCart()
            {
                menu_item = menuItemFromDb,
                menu_item_id = menuItemFromDb.id
            };

            return View(cartObj);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShoppingCart CartObject)
        {
            CartObject.id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.app_user_id = claim.Value;

                ShoppingCart cartFromDb = await _db.shopping_carts
                                                .Where(c => c.app_user_id == CartObject.app_user_id
                                                    && c.menu_item_id == CartObject.menu_item_id)
                                                .FirstOrDefaultAsync();

                if (cartFromDb == null)
                {
                    await _db.shopping_carts.AddAsync(CartObject);
                }
                else
                {
                    cartFromDb.count = cartFromDb.count + CartObject.count;
                }
                await _db.SaveChangesAsync();

                var count = _db.shopping_carts
                               .Where(c => c.app_user_id == CartObject.app_user_id)
                               .ToList()
                               .Count();
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, count);

                return RedirectToAction("Index");
            }
            else
            {

                var menuItemFromDb = await _db.menu_items
                                              .Include(m => m.category)
                                              .Include(m => m.subcategory)
                                              .Where(m => m.id == CartObject.menu_item_id)
                                              .FirstOrDefaultAsync();

                ShoppingCart cartObj = new ShoppingCart()
                {
                    menu_item = menuItemFromDb,
                    menu_item_id = menuItemFromDb.id
                };

                return View(cartObj);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
