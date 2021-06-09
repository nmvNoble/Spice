using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class Coupon
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Coupon Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string coupon_type { get; set; }

        public enum e_coupon_type { Percent = 0, Dollar = 1 }

        [Required]
        [Display(Name = "Discount")]
        public double discount { get; set; }

        [Required]
        [Display(Name = "Minimum Amount")]
        public double minimum_amount { get; set; }

        [Display(Name = "Image")]
        public byte[] picture { get; set; }

        [Display(Name = "Is Active")]
        public bool is_active { get; set; }
    }
}
