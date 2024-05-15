using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RentAppMVC.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        private readonly OrderLogic _orderLogic;
        private readonly OrderLineLogic _orderLineLogic;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(OrderLogic orderLogic, OrderLineLogic orderLineLogic, UserManager<IdentityUser> userManager)
        {
            _orderLogic = orderLogic;
            _userManager = userManager;
            _orderLineLogic = orderLineLogic;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.Message = TempData["SuccessMessage"].ToString();
            }
            else if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.Message = TempData["ErrorMessage"].ToString();
            }
            else
            {
                ViewBag.Message = "Your order has been processed successfully.";
            }

            return View();
        }


        [HttpPost("ProcessOrder")]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            try
            {
                var shoppingCart = CookieUtility.ReadCart(HttpContext);

                var currentUser = await _userManager.GetUserAsync(User);

                Order newOrder = new Order
                {
                    CustomerID = currentUser.Id,
                    OrderDate = DateTime.Now,
                    StartDate = shoppingCart.StartDate,
                    EndDate = shoppingCart.EndDate,
                    StartTime = shoppingCart.StartTime,
                    EndTime = shoppingCart.EndTime,
                    TotalHours = shoppingCart.TotalHours,
                    SubTotalPrice = shoppingCart.SubTotalPrice,
                    TotalOrderPrice = shoppingCart.TotalOrderPrice
                };

                int orderId = await _orderLogic.AddOrder(newOrder);

                if (orderId > 0)
                {
                    foreach (var orderLine in shoppingCart.Items)
                    {
                        orderLine.OrderID = orderId;

                        await _orderLineLogic.AddOrderLine(orderLine);
                    }
                }

                CookieUtility.EmptyCart(HttpContext);

                TempData["SuccessMessage"] = "Your order has been processed successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
                return RedirectToAction("Index");
            }
        }



        [HttpGet("Confirmation")]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
