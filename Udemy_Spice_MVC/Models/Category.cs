using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string name { get; set; }
    }
}
