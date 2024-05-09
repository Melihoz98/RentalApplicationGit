using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.Controllers
{
    [Route("Rent")]
    public class RentController : Controller
    {
        private readonly ShoppingCart _shoppingCart;

        public RentController(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var cartJson = Request.Cookies["shoppingCart"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cartData = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
                _shoppingCart.StartDate = cartData.StartDate;
                _shoppingCart.EndDate = cartData.EndDate;
                _shoppingCart.StartTime = cartData.StartTime;
                _shoppingCart.EndTime = cartData.EndTime;
                _shoppingCart.OrderLines = cartData.OrderLines; 
            }
            return View(_shoppingCart);
        }

        [HttpPost]
        public IActionResult ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            _shoppingCart.StartDate = startDate;
            _shoppingCart.EndDate = endDate;
            _shoppingCart.StartTime = startTime;
            _shoppingCart.EndTime = endTime;

            var cartJson = Request.Cookies["shoppingCart"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cartData = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
                _shoppingCart.OrderLines = cartData.OrderLines;
            }
            var updatedCartJson = JsonConvert.SerializeObject(_shoppingCart);
            Response.Cookies.Append("shoppingCart", updatedCartJson);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
