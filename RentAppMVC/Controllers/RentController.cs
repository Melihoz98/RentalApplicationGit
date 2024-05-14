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

        [HttpPost]
        public async Task<ActionResult> ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            var shoppingCart = CookieUtility.ReadCart(HttpContext);

            if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
            {
                shoppingCart.StartDate = startDate;
                shoppingCart.EndDate = endDate;
                shoppingCart.StartTime = startTime;
                shoppingCart.EndTime = endTime;
                CookieUtility.UpdateCart(HttpContext, shoppingCart);
            }

            int productId = shoppingCart.Product.ProductID;

            DateTime cartStartDate = shoppingCart.StartDate;
            DateTime cartEndDate = shoppingCart.EndDate;
            TimeSpan cartStartTime = shoppingCart.StartTime;
            TimeSpan cartEndTime = shoppingCart.EndTime;

            var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, cartStartDate, cartEndDate, cartStartTime, cartEndTime);
            if (availableProductCopies == null || availableProductCopies.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var firstProductCopy = availableProductCopies[0];
            Product product = await _productLogic.GetProductById(productId);
            OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber, product);
            shoppingCart.Items.Add(orderLine);

            CookieUtility.UpdateCart(HttpContext, shoppingCart);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
