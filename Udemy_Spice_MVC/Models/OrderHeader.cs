using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added
using System.ComponentModel.DataAnnotations.Schema;//added
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class OrderHeader
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual ApplicationUser app_user { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime order_date { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Original Total Order")]
        public double order_total_original { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double order_total { get; set; }

        [Required]
        [Display(Name = "Pickup Time")]
        public DateTime pickup_time { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Pickuo Date")]
        public DateTime pickup_date { get; set; }

        [Display(Name = "Coupon Code")]
        public string coupon_code { get; set; }

        [Display(Name = "Coupon Discount")]
        public double coupon_code_discount { get; set; }

        [Display(Name = "Order Status")]
        public string status { get; set; }

        [Display(Name = "Payment Status")]
        public string payment_status { get; set; }

        [Display(Name = "Order Comments")]
        public string comments { get; set; }

        [Display(Name = "Pickup Name")]
        public string pickup_name { get; set; }

        [Display(Name = "Phone Number")]
        public string phone_number { get; set; }

        [Display(Name = "Transaction ID")]
        public string transaction_id { get; set; }
    }
}
