using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added
using System.ComponentModel.DataAnnotations.Schema;//added
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class SubCategory
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public int category_id { get; set; }

        [Required]
        [Display(Name = "Sub-Category Name")]
        public string name { get; set; }

        //pertains to the corresponding row in the categories table based on category_id
        [ForeignKey("category_id")]
        public virtual Category category { get; set; }
    }
}
