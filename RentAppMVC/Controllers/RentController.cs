using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.Utilities;

namespace RentAppMVC.Controllers
{
    [Route("Rent")]
    public class RentController : Controller
    {
        private ShoppingCart _shoppingCart;
        private ProductLogic _productLogic;
        private readonly ProductCopyLogic _productCopyLogic;

        public RentController(ShoppingCart shoppingCart, ProductCopyLogic productCopyLogic, ProductLogic productLogic)
        {
            _shoppingCart = shoppingCart;
            _productCopyLogic = productCopyLogic;
            _productLogic = productLogic;
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


        //[HttpPost]
        //public IActionResult ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        //{
        //    _shoppingCart.StartDate = startDate;
        //    _shoppingCart.EndDate = endDate;
        //    _shoppingCart.StartTime = startTime;
        //    _shoppingCart.EndTime = endTime;

        //    CookieUtility.UpdateCart(HttpContext, _shoppingCart);

        //    // Check if _shoppingCart.Product is not null before redirecting
        //    if (_shoppingCart.Product != null)
        //    {
        //        // Redirect user to AddToCart method with the selected productId
        //        return RedirectToAction("AddToCart", "ShoppingCart", new { productId = _shoppingCart.Product.ProductID });
        //    }
        //    else
        //    {
        //        // If _shoppingCart.Product is null, redirect to some default action or handle the situation accordingly
        //        return RedirectToAction("Index", "ShoppingCart");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            // Læs indkøbskurven fra cookien
            var shoppingCart = CookieUtility.ReadCart(HttpContext);

            // Gem startdato, slutdato, starttid og sluttid i indkøbskurven, hvis de ikke allerede er angivet
            if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
            {
                shoppingCart.StartDate = startDate;
                shoppingCart.EndDate = endDate;
                shoppingCart.StartTime = startTime;
                shoppingCart.EndTime = endTime;
                CookieUtility.UpdateCart(HttpContext, shoppingCart);
            }

            // Få produktID fra indkøbskurven
            int productId = shoppingCart.Product.ProductID;

            // Hent startdato, slutdato, starttid og sluttid fra indkøbskurven
            DateTime cartStartDate = shoppingCart.StartDate;
            DateTime cartEndDate = shoppingCart.EndDate;
            TimeSpan cartStartTime = shoppingCart.StartTime;
            TimeSpan cartEndTime = shoppingCart.EndTime;

            // Få en liste over tilgængelige produktkopier baseret på produktID, startdato, slutdato, starttid og sluttid
            var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, cartStartDate, cartEndDate, cartStartTime, cartEndTime);
            if (availableProductCopies == null || availableProductCopies.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // Tilføj kun den første tilgængelige produktkopi til indkøbskurven
            var firstProductCopy = availableProductCopies[0];
            Product product = await _productLogic.GetProductById(productId);
            OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber, product);
            shoppingCart.Items.Add(orderLine);

            // Opdater indkøbskurven i cookien
            CookieUtility.UpdateCart(HttpContext, shoppingCart);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
