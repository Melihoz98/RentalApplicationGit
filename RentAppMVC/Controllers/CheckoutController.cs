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

        public CheckoutController(OrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
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
                int orderId = await _orderLogic.CreateOrder(order);

                if (orderId > 0)
                {
                    // Redirect til bekreftelsessiden med ordre-ID
                    return RedirectToAction("Confirmation", new { orderId });
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
                return RedirectToAction("Index");
            }
        }



        [HttpGet("Confirmation")]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            var order = await _orderLogic.GetOrderById(orderId);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("Index");
            }

            return View("Receipt", order);
        }


    }
}
