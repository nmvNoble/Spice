using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models.ViewModels
{
    public class OrderDetailsCart
    {
        [Display(Name = "List of Cart")]
        public List<ShoppingCart> listCart { get; set; }


        [Display(Name = "Order Header")]
        public OrderHeader orderHeader { get; set; }
    }
}
