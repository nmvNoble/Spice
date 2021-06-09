using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy_Spice_MVC.Models;//added

namespace Udemy_Spice_MVC.Utility
{
    public class SD//Static Details
    {
        public const string DefaultFoodImage = "default_food.png"; 

        public const string ManagerUser = "Manager";
        public const string KitchenUser = "Kitchen";
        public const string FrontDeskUser = "FrontDesk";
        public const string CustomerEndUser = "Customer";

        public const string ssShoppingCartCount = "ssCartCount";
        public const string ssCouponCode = "ssCouponCode";

        public const string StatusSubmitted = "Submitted";
        public const string StatusInProcess = "Being Prepared";
        public const string StatusReady = "Ready for Pickup";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static double DiscountedPrice(Coupon couponFromDb, double OriginalOrderTotal)
        {
            if (couponFromDb == null)
            {
                return OriginalOrderTotal;
            }
            else
            {
                if (couponFromDb.minimum_amount > OriginalOrderTotal)
                {
                    return OriginalOrderTotal;
                }
                else
                {
                    //everything is valid
                    if (Convert.ToInt32(couponFromDb.coupon_type) == (int)Coupon.e_coupon_type.Dollar)
                    {
                        //example: $10 off $100
                        return Math.Round(OriginalOrderTotal - couponFromDb.discount, 2);
                    }
                    if (Convert.ToInt32(couponFromDb.coupon_type) == (int)Coupon.e_coupon_type.Percent)
                    {
                        //example: 10% off $100 
                        return Math.Round(OriginalOrderTotal - (OriginalOrderTotal * (couponFromDb.discount / 100)), 2);
                    }
                }
            }
            return OriginalOrderTotal;
        }

    }
}