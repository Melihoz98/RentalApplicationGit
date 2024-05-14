using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentAppMVC.Models;
using RentAppMVC.Utilities;

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

        [HttpGet("Index")]
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
                _shoppingCart.Items = cartData.Items;
            }
            return View(_shoppingCart);
        }

        //[HttpPost]
        //public IActionResult ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        //{
        //    _shoppingCart.StartDate = startDate;
        //    _shoppingCart.EndDate = endDate;
        //    _shoppingCart.StartTime = startTime;
        //    _shoppingCart.EndTime = endTime;

        //    CookieUtility.UpdateCart(HttpContext, _shoppingCart);

        //    return RedirectToAction("Index", "ShoppingCart");
        //}

        //[HttpPost("ConfirmDateTime")]
        //public IActionResult ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        //{
        //    _shoppingCart.StartDate = startDate;
        //    _shoppingCart.EndDate = endDate;
        //    _shoppingCart.StartTime = startTime;
        //    _shoppingCart.EndTime = endTime;

        //    // Check if the cookies exist and are not null
        //    var cartJson = Request.Cookies["shoppingCart"];
        //    if (!string.IsNullOrEmpty(cartJson))
        //    {
        //        var cartData = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
        //        if (cartData != null) // Check if cartData is not null before accessing its properties
        //        {
        //            _shoppingCart.Items = cartData.Items;
        //        }
        //    }

        //    // Update the shopping cart in the cookie
        //    var updatedCartJson = JsonConvert.SerializeObject(_shoppingCart);
        //    Response.Cookies.Append("shoppingCart", updatedCartJson);

        //    return RedirectToAction("Index", "ShoppingCart");
        //}


        [HttpPost]
        public IActionResult ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            _shoppingCart.StartDate = startDate;
            _shoppingCart.EndDate = endDate;
            _shoppingCart.StartTime = startTime;
            _shoppingCart.EndTime = endTime;

            CookieUtility.UpdateCart(HttpContext, _shoppingCart);

            // Check if _shoppingCart.Product is not null before redirecting
            if (_shoppingCart.Product != null)
            {
                // Redirect user to AddToCart method with the selected productId
                return RedirectToAction("AddToCart", "ShoppingCart", new { productId = _shoppingCart.Product.ProductID });
            }
            else
            {
                // If _shoppingCart.Product is null, redirect to some default action or handle the situation accordingly
                return RedirectToAction("Index", "ShoppingCart");
            }
        }


    }
}
