using Microsoft.AspNetCore.Authorization;//added
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//added
using System;
using System.Collections.Generic;
using System.IO;//added
using System.Linq;
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added
using Udemy_Spice_MVC.Models;//added
using Udemy_Spice_MVC.Utility;//added

namespace Udemy_Spice_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Coupon couponBound { get; set; }

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.coupons.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    couponBound.picture = p1;
                }
                _db.coupons.Add(couponBound);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(couponBound);
        }

        //GET - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var coupon = await _db.coupons.SingleOrDefaultAsync(m => m.id == id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        //GET - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var coupon = await _db.coupons.SingleOrDefaultAsync(m => m.id == id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        //POST - Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST()
        {
            if (couponBound.id == 0)
            {
                return NotFound();
            }

            var couponFromDb = await _db.coupons.Where(c => c.id == couponBound.id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    couponFromDb.picture = p1;
                }
                couponFromDb.minimum_amount = couponBound.minimum_amount;
                couponFromDb.name = couponBound.name;
                couponFromDb.discount = couponBound.discount;
                couponFromDb.coupon_type = couponBound.coupon_type;
                couponFromDb.is_active = couponBound.is_active;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(couponBound);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var coupon = await _db.coupons.SingleOrDefaultAsync(m => m.id == id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var couponDelete = await _db.coupons.FindAsync(id);
            if (couponDelete != null)
            {
                _db.coupons.Remove(couponDelete);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
