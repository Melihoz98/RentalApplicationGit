using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RentAppMVC.Controllers
{
    [Route("ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private ShoppingCart _shoppingCart;
        private readonly ProductCopyLogic _productCopyLogic;
        private ProductLogic _productLogic;
        

        public ShoppingCartController(ShoppingCart shoppingCart, ProductCopyLogic productCopyLogic, ProductLogic productLogic)
        {
            _shoppingCart = shoppingCart;
            _productLogic = productLogic;
            _productCopyLogic = productCopyLogic;
            
        }

        public IActionResult Index()
        {
            _shoppingCart = CookieUtility.ReadCart(HttpContext);
            return View(_shoppingCart);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {
            var shoppingCart = CookieUtility.ReadCart(HttpContext);

            if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
            {
                shoppingCart.Product = await _productLogic.GetProductById(productId);
                CookieUtility.UpdateCart(HttpContext, shoppingCart);
                return RedirectToAction("Index", "Rent");
            }

            DateTime startDate = shoppingCart.StartDate;
            DateTime endDate = shoppingCart.EndDate;
            TimeSpan startTime = shoppingCart.StartTime;
            TimeSpan endTime = shoppingCart.EndTime;

            var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, startDate, endDate, startTime, endTime);
            if (availableProductCopies == null || availableProductCopies.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var firstProductCopy = availableProductCopies[0];
            Product product = await _productLogic.GetProductById(productId);
            OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber, product);
            shoppingCart.Items.Add(orderLine);

            CookieUtility.UpdateCart(HttpContext, shoppingCart);

            return RedirectToAction("Index");
        }

        [HttpPost("RemoveItem")]
        public ActionResult RemoveItem(string serialNumber)
        {
            _shoppingCart = CookieUtility.ReadCart(HttpContext);
            _shoppingCart.RemoveItem(serialNumber);
            CookieUtility.UpdateCart(HttpContext, _shoppingCart);
            return RedirectToAction("Index");
        }

        [HttpPost("EmptyCart")]
        public ActionResult EmptyCart()
        {
            CookieUtility.EmptyCart(HttpContext);
            return RedirectToAction("Index");
        }
    }
}
