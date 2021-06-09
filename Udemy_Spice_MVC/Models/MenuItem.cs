using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added
using System.ComponentModel.DataAnnotations.Schema;//added
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class MenuItem
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Menu Item Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Spicyness")]
        public string spicyness { get; set; }
        public enum e_spicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

        [Display(Name = "Image")]
        public string image { get; set; }

        [Display(Name = "Category")]
        public int category_id { get; set; }

        [ForeignKey("category_id")]
        public virtual Category category { get; set; }

        [Display(Name = "Sub-Category")]
        public int subcategory_id { get; set; }

        [ForeignKey("subcategory_id")]
        public virtual SubCategory subcategory { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = " Price should be greater than ${1}")]
        [Display(Name = "Price")]
        public double price { get; set; }

    }
}
