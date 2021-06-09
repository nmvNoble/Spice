using Microsoft.AspNetCore.Http;//added
using Microsoft.AspNetCore.Identity.UI.Services;//added
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//added
using Stripe;//added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;//added
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added
using Udemy_Spice_MVC.Models;//added
using Udemy_Spice_MVC.Models.ViewModels;//added
using Udemy_Spice_MVC.Utility;//added

namespace Udemy_Spice_MVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public OrderDetailsCart detailCart { get; set; }

        public CartController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            detailCart = new OrderDetailsCart()
            {
                orderHeader = new Models.OrderHeader()
            };

            detailCart.orderHeader.order_total = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
             
            var cart = _db.shopping_carts.Where(c => c.app_user_id == claim.Value);
            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }

            foreach (var list in detailCart.listCart)
            {
                list.menu_item = await _db.menu_items.FirstOrDefaultAsync(m => m.id == list.menu_item_id);
                detailCart.orderHeader.order_total = detailCart.orderHeader.order_total + (list.menu_item.price * list.count);
                list.menu_item.description = SD.ConvertToRawHtml(list.menu_item.description);
                if (list.menu_item.description.Length > 100)
                {
                    list.menu_item.description = list.menu_item.description.Substring(0, 99) + "...";
                }
            }
            detailCart.orderHeader.order_total_original = detailCart.orderHeader.order_total;

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                detailCart.orderHeader.coupon_code = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.coupons.Where(c => c.name.ToLower() == detailCart.orderHeader.coupon_code.ToLower()).FirstOrDefaultAsync();
                detailCart.orderHeader.order_total = SD.DiscountedPrice(couponFromDb, detailCart.orderHeader.order_total_original);
            }


            return View(detailCart);
        }

        public async Task<IActionResult> Summary()
        {
            detailCart = new OrderDetailsCart()
            {
                orderHeader = new Models.OrderHeader()
            };

            detailCart.orderHeader.order_total = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = await _db.app_users.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();

            var cart = _db.shopping_carts.Where(c => c.app_user_id == claim.Value);
            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }

            foreach (var list in detailCart.listCart)
            {
                list.menu_item = await _db.menu_items.FirstOrDefaultAsync(m => m.id == list.menu_item_id);
                detailCart.orderHeader.order_total = detailCart.orderHeader.order_total + (list.menu_item.price * list.count);
            }
            detailCart.orderHeader.order_total_original = detailCart.orderHeader.order_total;
            detailCart.orderHeader.pickup_name = applicationUser.Name;
            detailCart.orderHeader.phone_number = applicationUser.PhoneNumber;
            detailCart.orderHeader.pickup_time = DateTime.Now;


            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                detailCart.orderHeader.coupon_code = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.coupons.Where(c => c.name.ToLower() == detailCart.orderHeader.coupon_code.ToLower()).FirstOrDefaultAsync();
                detailCart.orderHeader.order_total = SD.DiscountedPrice(couponFromDb, detailCart.orderHeader.order_total_original);
            }


            return View(detailCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPOST(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            detailCart.listCart = await _db.shopping_carts.Where(c => c.app_user_id == claim.Value).ToListAsync();

            detailCart.orderHeader.payment_status = SD.PaymentStatusPending;
            detailCart.orderHeader.order_date = DateTime.Now;
            detailCart.orderHeader.user_id = claim.Value;
            detailCart.orderHeader.status = SD.PaymentStatusPending;
            detailCart.orderHeader.pickup_time = Convert.ToDateTime(
                                                         detailCart.orderHeader.pickup_date.ToShortDateString() 
                                                         + " " 
                                                         + detailCart.orderHeader.pickup_time.ToShortTimeString());

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            _db.order_headers.Add(detailCart.orderHeader);
            await _db.SaveChangesAsync();

            detailCart.orderHeader.order_total_original = 0;


            foreach (var item in detailCart.listCart)
            {
                item.menu_item = await _db.menu_items.FirstOrDefaultAsync(m => m.id == item.menu_item_id);
                OrderDetails orderDetails = new OrderDetails
                {
                    menu_item_id = item.menu_item_id,
                    order_id = detailCart.orderHeader.id,
                    description = item.menu_item.description,
                    name = item.menu_item.name,
                    price = item.menu_item.price,
                    count = item.count
                };
                detailCart.orderHeader.order_total_original += orderDetails.count * orderDetails.price;
                _db.order_details.Add(orderDetails);

            }

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            { 
                detailCart.orderHeader.coupon_code = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.coupons.Where(c => c.name.ToLower() == detailCart.orderHeader.coupon_code.ToLower()).FirstOrDefaultAsync();
                detailCart.orderHeader.order_total = SD.DiscountedPrice(couponFromDb, detailCart.orderHeader.order_total_original);
            }

            else
            {
                detailCart.orderHeader.order_total = detailCart.orderHeader.order_total_original;
            }
            detailCart.orderHeader.coupon_code_discount = detailCart.orderHeader.order_total_original - detailCart.orderHeader.order_total;

            _db.shopping_carts.RemoveRange(detailCart.listCart);
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, 0);
            await _db.SaveChangesAsync();

            
            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(detailCart.orderHeader.order_total * 100),
                Currency = "usd",
                Description = "Order ID : " + detailCart.orderHeader.id,
                Source = stripeToken

            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.BalanceTransactionId == null)
            {
                detailCart.orderHeader.payment_status = SD.PaymentStatusRejected;
            }
            else
            {
                detailCart.orderHeader.transaction_id = charge.BalanceTransactionId;
            }

            if (charge.Status.ToLower() == "succeeded")
            {
                detailCart.orderHeader.payment_status = SD.PaymentStatusApproved;
                detailCart.orderHeader.status = SD.StatusSubmitted;
            }
            else
            {
                detailCart.orderHeader.payment_status = SD.PaymentStatusRejected;
            }
            await _db.SaveChangesAsync();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Confirm", "Order", new { id = detailCart.orderHeader.id });
        }


        public IActionResult AddCoupon()
        {
            if (detailCart.orderHeader.coupon_code == null)
            {
                detailCart.orderHeader.coupon_code = "";
            }
            HttpContext.Session.SetString(SD.ssCouponCode, detailCart.orderHeader.coupon_code);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveCoupon()
        {

            HttpContext.Session.SetString(SD.ssCouponCode, string.Empty);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Plus(int cartId)
        {
            var cart = await _db.shopping_carts.FirstOrDefaultAsync(c => c.id == cartId);
            cart.count += 1;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int cartId)
        {
            var cart = await _db.shopping_carts.FirstOrDefaultAsync(c => c.id == cartId);
            if (cart.count == 1)
            {
                _db.shopping_carts.Remove(cart);
                await _db.SaveChangesAsync();

                var cnt = _db.shopping_carts.Where(u => u.app_user_id == cart.app_user_id).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);
            }
            else
            {
                cart.count -= 1;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int cartId)
        {
            var cart = await _db.shopping_carts.FirstOrDefaultAsync(c => c.id == cartId);

            _db.shopping_carts.Remove(cart);
            await _db.SaveChangesAsync();

            var cnt = _db.shopping_carts.Where(u => u.app_user_id == cart.app_user_id).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);


            return RedirectToAction(nameof(Index));
        }
    }
}
