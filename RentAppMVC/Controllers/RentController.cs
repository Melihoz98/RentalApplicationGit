using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
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
            var cartJson = Request.Cookies["shoppingCartCookie"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cartData = JsonConvert.DeserializeObject<ShoppingCart.ShoppingCartData>(cartJson);
                _shoppingCart.StartDate = cartData.StartDate;
                _shoppingCart.EndDate = cartData.EndDate;
                _shoppingCart.StartTime = cartData.StartTime;
                _shoppingCart.EndTime = cartData.EndTime;
            }
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmRent(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            _shoppingCart.StartDate = startDate;
            _shoppingCart.EndDate = endDate;
            _shoppingCart.StartTime = startTime;
            _shoppingCart.EndTime = endTime;

            var cartJson = JsonConvert.SerializeObject(_shoppingCart);
            Response.Cookies.Append("shoppingCartCookie", cartJson);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
