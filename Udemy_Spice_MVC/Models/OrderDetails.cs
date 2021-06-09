using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class OrderDetails
    {
        [Key]
        [Required]
        [Display(Name = "Pickup Time")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Order ID")]
        public int order_id { get; set; }

        [ForeignKey("order_id")]
        public virtual OrderHeader order_header { get; set; }

        [Required]
        [Display(Name = "Menu Item ID")]
        public int menu_item_id { get; set; }

        [ForeignKey("menu_item_id")]
        public virtual MenuItem menu_item { get; set; }

        [Display(Name = "Count")]
        public int count { get; set; }

        [Display(Name = "Order Detail Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double price { get; set; }
    }
}
