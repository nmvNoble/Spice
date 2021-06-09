using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models.ViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Menu Item")]
        public IEnumerable<MenuItem> menuItem { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<Category> category { get; set; }

        [Display(Name = "Coupon")]
        public IEnumerable<Coupon> coupon { get; set; }
    }
}
