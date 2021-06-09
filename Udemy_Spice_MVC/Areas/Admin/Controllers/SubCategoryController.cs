using Microsoft.AspNetCore.Authorization;//added
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;//added
using Microsoft.EntityFrameworkCore;//added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added
using Udemy_Spice_MVC.Models;//added
using Udemy_Spice_MVC.Models.ViewModels;//added
using Udemy_Spice_MVC.Utility;//added

namespace Udemy_Spice_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        [TempData]
        public string statusMessage { get; set; }

        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Index
        public async Task<IActionResult> Index()
        {
            return View(await _db.subcategories.Include(s => s.category).ToListAsync());
        }

        //GET - Create
        public async Task<IActionResult> Create()
        {
            SubcategoryAndCategoryViewModel model = new SubcategoryAndCategoryViewModel()
            {
                categoryList = await _db.categories.ToListAsync(),
                subcategory = new Models.SubCategory(),
                subcategoryList = await _db.subcategories.OrderBy(p => p.name)
                                                         .Select(p => p.name)
                                                         .Distinct()
                                                         .ToListAsync()
            };
            return View(model);
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubcategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubcartegoryExist = _db.subcategories.Include(s => s.category)
                                                             .Where(s => s.name == model.subcategory.name &&
                                                                    s.category.id == model.subcategory.category_id);
                if (doesSubcartegoryExist.Count() > 0)
                {
                    //error
                    statusMessage = "Error: Sub-Category already exists under " +
                                    doesSubcartegoryExist.First().category.name +
                                    " category. Please use another name.";
                }
                else
                {
                    _db.subcategories.Add(model.subcategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubcategoryAndCategoryViewModel modelVM = new SubcategoryAndCategoryViewModel()
            {
                categoryList = await _db.categories.ToListAsync(),
                subcategory = new Models.SubCategory(),
                subcategoryList = await _db.subcategories.OrderBy(p => p.name)
                                                         .Select(p => p.name)
                                                         //.Distinct()
                                                         .ToListAsync(),
                statusMessage = statusMessage
            };
            return View(modelVM);
        }



        [ActionName("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subcategories = new List<SubCategory>();

            subcategories = await (from subcategory in _db.subcategories
                                   where subcategory.category_id == id
                                   select subcategory).ToListAsync();
            return Json(new SelectList(subcategories, "id", "name"));
        }

        //GET - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcategory = await _db.subcategories.SingleOrDefaultAsync(m => m.id == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            SubcategoryAndCategoryViewModel model = new SubcategoryAndCategoryViewModel()
            {
                categoryList = await _db.categories.ToListAsync(),
                subcategory = subcategory,
                subcategoryList = await _db.subcategories.OrderBy(p => p.name)
                                                         .Select(p => p.name)
                                                         .Distinct()
                                                         .ToListAsync()
            };
            return View(model);
        }

        //GET - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcategory = await _db.subcategories.SingleOrDefaultAsync(m => m.id == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            SubcategoryAndCategoryViewModel model = new SubcategoryAndCategoryViewModel()
            {
                categoryList = await _db.categories.ToListAsync(),
                subcategory = subcategory,
                subcategoryList = await _db.subcategories.OrderBy(p => p.name)
                                                         .Select(p => p.name)
                                                         .Distinct()
                                                         .ToListAsync()
            };
            return View(model);
        }

        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubcategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubcartegoryExist = _db.subcategories.Include(s => s.category)
                                                             .Where(s => s.name == model.subcategory.name &&
                                                                    s.category.id == model.subcategory.category_id);
                if (doesSubcartegoryExist.Count() > 0)
                {
                    //error
                    statusMessage = "Error: Sub-Category already exists under " +
                                    doesSubcartegoryExist.First().category.name +
                                    " category. Please use another name.";
                }
                else
                {
                    var subcategoryFromDb = await _db.subcategories.FindAsync(model.subcategory.id);
                    subcategoryFromDb.name = model.subcategory.name;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubcategoryAndCategoryViewModel modelVM = new SubcategoryAndCategoryViewModel()
            {
                categoryList = await _db.categories.ToListAsync(),
                subcategory = new Models.SubCategory(),
                subcategoryList = await _db.subcategories.OrderBy(p => p.name)
                                                         .Select(p => p.name)
                                                         //.Distinct()
                                                         .ToListAsync(),
                statusMessage = statusMessage
            };
            return View(modelVM);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcategory = await _db.subcategories.SingleOrDefaultAsync(m => m.id == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            SubcategoryAndCategoryViewModel model = new SubcategoryAndCategoryViewModel()
            {
                categoryList = await _db.categories.ToListAsync(),
                subcategory = subcategory,
                subcategoryList = await _db.subcategories.OrderBy(p => p.name)
                                                         .Select(p => p.name)
                                                         .Distinct()
                                                         .ToListAsync()
            };
            return View(model);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcategory = await _db.subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return View();
            }
            _db.subcategories.Remove(subcategory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
