using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderHeader orderHeader { get; set; }
        public List<OrderDetails> orderDetails { get; set; }
    }
}
