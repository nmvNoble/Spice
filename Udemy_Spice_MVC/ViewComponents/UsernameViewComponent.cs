using Microsoft.AspNetCore.Mvc;//added
using Microsoft.EntityFrameworkCore;//added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;//added
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added

namespace Udemy_Spice_MVC.ViewComponents
{
    [ViewComponent]
    public class UsernameViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public UsernameViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userFromDb = await _db.app_users.FirstOrDefaultAsync(u => u.Id == claims.Value);

            return View(userFromDb);
        }
    }
}
