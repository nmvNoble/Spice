using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added
using System.ComponentModel.DataAnnotations.Schema;//added
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            count = 1;
        }

        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Application User ID")]
        public string app_user_id { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser app_user { get; set; }

        [Display(Name = "Menu Item ID")]
        public int menu_item_id { get; set; }

        [NotMapped]
        [ForeignKey("MenuItemId")]
        public virtual MenuItem menu_item { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than or equal to {1}")]
        [Display(Name = "Count")]
        public int count { get; set; }
    }
}
