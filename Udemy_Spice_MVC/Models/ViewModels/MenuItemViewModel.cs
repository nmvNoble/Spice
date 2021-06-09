using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models.ViewModels
{
    public class MenuItemViewModel
    {

        [Display(Name = "Manu Item")]
        public MenuItem menuItem { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<Category> category { get; set; }

        [Display(Name = "Sub-Category")]
        public IEnumerable<SubCategory> subcategory { get; set; }
    }
}
