using Microsoft.AspNetCore.Authorization;//added
using Microsoft.AspNetCore.Hosting;//added
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//added
using System;
using System.Collections.Generic;
using System.IO;//added
using System.Linq;
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added
using Udemy_Spice_MVC.Models.ViewModels;//added
using Udemy_Spice_MVC.Utility;//added

namespace Udemy_Spice_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public MenuItemViewModel menuItemVM { get; set; }

        public MenuItemController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            menuItemVM = new MenuItemViewModel()
            {
                category = _db.categories,
                menuItem = new Models.MenuItem()
            };
        }

        public async Task<IActionResult> Index()
        {

            var menuItems = await _db.menu_items.Include(m => m.category).Include(m => m.subcategory).ToListAsync();
            return View(menuItems);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View(menuItemVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]//subCategoryId is in the Create.cshtml of the same name="subCategoryId"
        public async Task<IActionResult> CreatePOST()
        {
            var subCategoryId = Convert.ToInt32(Request.Form["subCategoryId"].ToString());
            menuItemVM.menuItem.subcategory_id = subCategoryId;

            if (!ModelState.IsValid)
            {
                return View(menuItemVM);
            }

            _db.menu_items.Add(menuItemVM.menuItem);
            await _db.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.menu_items.FindAsync(menuItemVM.menuItem.id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, menuItemVM.menuItem.id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.image = @"\images\" + menuItemVM.menuItem.id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultFoodImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + menuItemVM.menuItem.id + ".png");
                menuItemFromDb.image = @"\images\" + menuItemVM.menuItem.id + ".png";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            menuItemVM.menuItem = await _db.menu_items
                                           .Include(m => m.category)
                                           .Include(m => m.subcategory)
                                           .SingleOrDefaultAsync(m => m.id == id);
            menuItemVM.subcategory = await _db.subcategories
                                              .Where(s => s.category_id == menuItemVM.menuItem.category_id)
                                              .ToListAsync();

            if (menuItemVM.menuItem == null)
            {
                return NotFound();
            }
            return View(menuItemVM);
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            menuItemVM.menuItem = await _db.menu_items
                                           .Include(m => m.category)
                                           .Include(m => m.subcategory)
                                           .SingleOrDefaultAsync(m => m.id == id);
            menuItemVM.subcategory = await _db.subcategories
                                              .Where(s => s.category_id == menuItemVM.menuItem.category_id)
                                              .ToListAsync();

            if (menuItemVM.menuItem == null)
            {
                return NotFound();
            }
            return View(menuItemVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            menuItemVM.menuItem.subcategory_id = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                menuItemVM.subcategory = await _db.subcategories
                                                  .Where(s => s.category_id == menuItemVM.menuItem.category_id)
                                                  .ToListAsync();
                return View(menuItemVM);
            }

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.menu_items.FindAsync(menuItemVM.menuItem.id);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, menuItemFromDb.image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //we will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, 
                                                                     menuItemVM.menuItem.id + extension_new), 
                                                                     FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.image = @"\images\" + menuItemVM.menuItem.id + extension_new;
            }

            menuItemFromDb.name = menuItemVM.menuItem.name;
            menuItemFromDb.description = menuItemVM.menuItem.description;
            menuItemFromDb.price = menuItemVM.menuItem.price;
            menuItemFromDb.spicyness = menuItemVM.menuItem.spicyness;
            menuItemFromDb.category_id = menuItemVM.menuItem.category_id;
            menuItemFromDb.subcategory_id = menuItemVM.menuItem.subcategory_id;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            menuItemVM.menuItem = await _db.menu_items
                                           .Include(m => m.category)
                                           .Include(m => m.subcategory)
                                           .SingleOrDefaultAsync(m => m.id == id);
            menuItemVM.subcategory = await _db.subcategories
                                              .Where(s => s.category_id == menuItemVM.menuItem.category_id)
                                              .ToListAsync();

            if (menuItemVM.menuItem == null)
            {
                return NotFound();
            }
            return View(menuItemVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var menuItem = await _db.menu_items.FindAsync(id);
            if (menuItem != null)
            {
                var imagePath = Path.Combine(webRootPath, menuItem.image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.menu_items.Remove(menuItem);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
