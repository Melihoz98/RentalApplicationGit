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

        //[HttpPost("AddToCart")]
        //public async Task<ActionResult> AddToCart(int productId)
        //{
        //    // Læs startdato, slutdato, starttid og sluttid fra cookien
        //    var startDateCookie = CookieUtility.GetCookieValue(HttpContext, "StartDate");
        //    var startTimeCookie = CookieUtility.GetCookieValue(HttpContext, "StartTime");
        //    var endDateCookie = CookieUtility.GetCookieValue(HttpContext, "EndDate");
        //    var endTimeCookie = CookieUtility.GetCookieValue(HttpContext, "EndTime");

        //    //  Kontroller om startdato og starttid er tilgængelige
        //    if (string.IsNullOrEmpty(startDateCookie) || string.IsNullOrEmpty(startTimeCookie))
        //    {
        //        // Hvis startdato og starttid ikke er tilgængelige, omdiriger til Rent / Index for at indtaste dem
        //        return RedirectToAction("Index", "Rent");
        //    }

        //    // Parse startdato og starttid
        //    DateTime startDate = DateTime.Parse(startDateCookie);
        //    TimeSpan startTime = TimeSpan.Parse(startTimeCookie);

        //    // Parse slutdato og sluttid, hvis de er tilgængelige, ellers brug startdato og starttid
        //    DateTime endDate = string.IsNullOrEmpty(endDateCookie) ? startDate : DateTime.Parse(endDateCookie);
        //    TimeSpan endTime = string.IsNullOrEmpty(endTimeCookie) ? startTime : TimeSpan.Parse(endTimeCookie);

        //    // Få en liste over tilgængelige produktkopier baseret på produktID, startdato, slutdato, starttid og sluttid
        //    var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, startDate, endDate, startTime, endTime);
        //    if (availableProductCopies == null || availableProductCopies.Count == 0)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    // Tilføj kun den første tilgængelige produktkopi til indkøbskurven
        //    var firstProductCopy = availableProductCopies[0];
        //    OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber);
        //    _shoppingCart.Items.Add(orderLine);

        //    // Opdater indkøbskurven i cookien
        //    CookieUtility.UpdateCart(HttpContext, _shoppingCart);
        //    return RedirectToAction("Index");
        //}



        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {
            // Læs indkøbskurven fra cookien
            var shoppingCart = CookieUtility.ReadCart(HttpContext);

            // Hvis indkøbskurven er tom, eller datoer/tider ikke er indstillet, omdiriger til Rent/Index for at indtaste dem
            if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
            {
                // Gem produktet i cookien og omdiriger til Rent/Index
                shoppingCart.Product = await _productLogic.GetProductById(productId);
                CookieUtility.UpdateCart(HttpContext, shoppingCart);
                return RedirectToAction("Index", "Rent");
            }

            // Hent startdato, slutdato, starttid og sluttid fra indkøbskurven
            DateTime startDate = shoppingCart.StartDate;
            DateTime endDate = shoppingCart.EndDate;
            TimeSpan startTime = shoppingCart.StartTime;
            TimeSpan endTime = shoppingCart.EndTime;

            // Få en liste over tilgængelige produktkopier baseret på produktID, startdato, slutdato, starttid og sluttid
            var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, startDate, endDate, startTime, endTime);
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

            return RedirectToAction("Index");
        }



        [HttpPost("RemoveItem")]
        public ActionResult RemoveItem(string serialNumber)
        {
            _shoppingCart.RemoveItem(serialNumber);
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
