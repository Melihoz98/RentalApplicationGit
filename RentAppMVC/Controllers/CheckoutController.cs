using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
using System;
using System.Threading.Tasks;

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
            // Vis checkout-visningen
            return View();
        }

        [HttpPost("ProcessOrder")]
        public async Task<ActionResult> ProcessOrder(Order order)
        {
            // Gem ordren i databasen
            try
            {
                await _orderLogic.AddOrder(order);
                // Tøm indkøbskurven efter ordren er gemt
                CookieUtility.EmptyCart(HttpContext);
                // Redirect til en bekræftelsesside eller en anden relevant side
                return RedirectToAction("Confirmation");
            }
            catch (Exception ex)
            {
                // Håndter fejl og vis en fejlmeddelelse
                TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet("Confirmation")]
        public IActionResult Confirmation()
        {
            // Vis en bekræftelsesside
            return View();
        }
    }
}
