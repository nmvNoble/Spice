using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models.ViewModels
{
    public class SubcategoryAndCategoryViewModel
    {

        [Display(Name = "Category List")]
        public IEnumerable<Category> categoryList { get; set; }

        [Display(Name = "Sub-Category")]
        public SubCategory subcategory { get; set; }

        [Display(Name = "Sub-Category List")]
        public List<string> subcategoryList { get; set; }

        [Display(Name = "Status Message")]
        public string statusMessage { get; set; }
    }
}
