using Microsoft.AspNetCore.Authorization;//added
using Microsoft.AspNetCore.Identity.UI.Services;//added
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;//added
using System.Threading.Tasks;
using Udemy_Spice_MVC.Data;//added
using Udemy_Spice_MVC.Models;//added
using Udemy_Spice_MVC.Models.ViewModels;//added
using Udemy_Spice_MVC.Utility;//added

namespace Udemy_Spice_MVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private int pageSize = 2;
        private readonly IEmailSender _emailSender;

        public OrderController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Confirm(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel()
            {
                orderHeader = await _db.order_headers.Include(o => o.app_user)
                        .FirstOrDefaultAsync(o => o.id == id && o.user_id == claim.Value),
                orderDetails = await _db.order_details.Where(o => o.order_id == id).ToListAsync()
            };

            return View(orderDetailsViewModel);
        }

        [Authorize]
        public async Task<IActionResult> OrderHistory(int productPage = 1)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            OrderListViewModel orderListVM = new OrderListViewModel()
            {
                Orders = new List<OrderDetailsViewModel>()
            };

            List<OrderHeader> orderHeaderList = await _db.order_headers.Include(o => o.app_user)
                                                                       .Where(u => u.user_id == claim.Value)
                                                                       .ToListAsync();

            foreach (OrderHeader item in orderHeaderList)
            {
                OrderDetailsViewModel individual = new OrderDetailsViewModel
                {
                    orderHeader = item,
                    orderDetails = await _db.order_details.Where(o => o.order_id == item.id).ToListAsync()
                };
                orderListVM.Orders.Add(individual);
            }
            var count = orderListVM.Orders.Count;
            orderListVM.Orders = orderListVM.Orders.OrderByDescending(p => p.orderHeader.id)
                                                   .Skip((productPage - 1) * pageSize)
                                                   .Take(pageSize)
                                                   .ToList();

            orderListVM.PagingInfo = new PagingInfo
            {
                currentPage = productPage,
                itemsPerPage = pageSize,
                totalItem = count,
                urlParam = "/Customer/Order/OrderHistory?productPage=:"
            };

            return View(orderListVM);
        }

        [Authorize(Roles = SD.KitchenUser + ", " + SD.ManagerUser)]
        public async Task<IActionResult> ManageOrder()
        {
            List<OrderDetailsViewModel> orderDetailsVM = new List<OrderDetailsViewModel>();
            List<OrderHeader> orderHeaderList = await _db.order_headers.Where(o => o.status == SD.StatusSubmitted 
                                                                                || o.status == SD.StatusInProcess)
                                                                       .OrderByDescending(o=>o.pickup_time)
                                                                       .ToListAsync();

            foreach (OrderHeader item in orderHeaderList)
            {
                OrderDetailsViewModel individual = new OrderDetailsViewModel
                {
                    orderHeader = item,
                    orderDetails = await _db.order_details.Where(o => o.order_id == item.id).ToListAsync()
                };
                orderDetailsVM.Add(individual);
            }


            //List<OrderDetailsViewModel> resultOrderDetailsVM = (List<OrderDetailsViewModel>)orderDetailsVM.OrderBy(o => o.orderHeader.pickup_time);
            return View(orderDetailsVM.OrderBy(o => o.orderHeader.pickup_time).ToList());
        }

        //Used for Modals in Order History
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel()
            {
                orderHeader = await _db.order_headers.Include(el => el.app_user).FirstOrDefaultAsync(m => m.id == id),
                orderDetails = await _db.order_details.Where(m => m.order_id == id).ToListAsync()
            };
            //orderDetailsViewModel.OrderHeader.ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == orderDetailsViewModel.OrderHeader.UserId);

            return PartialView("_IndividualOrderDetails", orderDetailsViewModel);
        }

        //Used for Modals in Order History
        public async Task<IActionResult> GetOrderStatus(int id)
        {
            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel()
            {
                orderHeader = await _db.order_headers.Include(el => el.app_user).FirstOrDefaultAsync(m => m.id == id),
                orderDetails = await _db.order_details.Where(m => m.order_id == id).ToListAsync()
            };
            //orderDetailsViewModel.OrderHeader.ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == orderDetailsViewModel.OrderHeader.UserId);

            return PartialView("_OrderStatus", orderDetailsViewModel);
        }
        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> OrderPrepare(int orderId)
        {
            OrderHeader orderHeader = await _db.order_headers.FindAsync(orderId);
            orderHeader.status = SD.StatusInProcess;
            /*//Email logic to notify user that order is ready for pickup
            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.user_id)
                                                       .FirstOrDefault().Email
                                              , "Spice - Order being Prepared " + orderHeader.id.ToString()
                                              , "Order is being prepared for pickup.");//*/
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageOrder", "Order");
        }


        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> OrderReady(int orderId)
        {
            OrderHeader orderHeader = await _db.order_headers.FindAsync(orderId);
            orderHeader.status = SD.StatusReady;
            /*//Email logic to notify user that order is ready for pickup
            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.user_id)
                                                       .FirstOrDefault().Email
                                              , "Spice - Order Ready for Pickup " + orderHeader.id.ToString()
                                              , "Order is ready for pickup.");//*/
            await _db.SaveChangesAsync();

            

            return RedirectToAction("ManageOrder", "Order");
        }


        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> OrderCancel(int orderId)
        {
            OrderHeader orderHeader = await _db.order_headers.FindAsync(orderId);
            orderHeader.status = SD.StatusCancelled;
            await _db.SaveChangesAsync();
            /*//SendGrid - Email logic to notify user that order is ready for pickup
            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.user_id)
                                                       .FirstOrDefault().Email
                                              , "Spice - Order Ready for Pickup " + orderHeader.id.ToString()
                                              , "Order is ready for pickup.");//*/
            /*//TEMPORARY EMS
            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.user_id)
                                                       .FirstOrDefault().Email
                                              , "Spice - Order Ready for Pickup " + orderHeader.id.ToString()
                                              , "Order is ready for pickup.");//*/
            return RedirectToAction("ManageOrder", "Order");
        }

        [Authorize]
        public async Task<IActionResult> OrderPickup(int productPage = 1
                                                   , string searchName = null
                                                   , string searchPhone = null
                                                   , string searchEmail = null)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            OrderListViewModel orderListVM = new OrderListViewModel()
            {
                Orders = new List<OrderDetailsViewModel>()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Customer/Order/OrderPickup?productPage=:");
            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }
            param.Append("&searchPhone=");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }
            param.Append("&searchEmail=");
            if (searchEmail != null)
            {
                param.Append(searchEmail);
            }

            List<OrderHeader> orderHeaderList = new List<OrderHeader>();
            if (searchName != null || searchPhone != null || searchEmail != null)
            {
                var user = new ApplicationUser();
                if (searchName != null)
                {
                    orderHeaderList = await _db.order_headers
                                               .Include(o => o.app_user)
                                               .Where(u => u.pickup_name.ToLower().Contains(searchName.ToLower()))
                                               .OrderByDescending(o => o.order_date)
                                               .ToListAsync();
                }
                else
                {
                    if (searchEmail != null)
                    {
                        user = await _db.app_users.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower())).FirstOrDefaultAsync();
                        orderHeaderList = await _db.order_headers
                                                   .Include(o => o.app_user)
                                                   .Where(o => o.user_id == user.Id)
                                                   .OrderByDescending(o => o.order_date)
                                                   .ToListAsync();
                    }
                    else
                    {
                        if (searchPhone != null)
                        {
                            orderHeaderList = await _db.order_headers
                                                       .Include(o => o.app_user)
                                                       .Where(u => u.phone_number.Contains(searchPhone))
                                                       .OrderByDescending(o => o.order_date)
                                                       .ToListAsync();
                        }
                    }
                }
            }
            else
            {
                orderHeaderList = await _db.order_headers.Include(o => o.app_user)
                                                                           .Where(u => u.status == SD.StatusReady)
                                                                           .ToListAsync();
            }
                foreach (OrderHeader item in orderHeaderList)
                {
                    OrderDetailsViewModel individual = new OrderDetailsViewModel
                    {
                        orderHeader = item,
                        orderDetails = await _db.order_details.Where(o => o.order_id == item.id).ToListAsync()
                    };
                    orderListVM.Orders.Add(individual);
                }
            
            var count = orderListVM.Orders.Count;
            orderListVM.Orders = orderListVM.Orders.OrderByDescending(p => p.orderHeader.id)
                                                   .Skip((productPage - 1) * pageSize)
                                                   .Take(pageSize)
                                                   .ToList();

            orderListVM.PagingInfo = new PagingInfo
            {
                currentPage = productPage,
                itemsPerPage = pageSize,
                totalItem = count,
                urlParam = param.ToString()
            };

            return View(orderListVM);
        }

        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        [HttpPost]
        [ActionName("OrderPickup")]
        public async Task<IActionResult> OrderPickupPOST(int orderId)
        {
            OrderHeader orderHeader = await _db.order_headers.FindAsync(orderId);
            orderHeader.status = SD.StatusCompleted;
            await _db.SaveChangesAsync();
            return RedirectToAction("OrderPickup", "Order");
        }
    }
}
