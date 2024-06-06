//using Microsoft.AspNetCore.Mvc;
//using RentAppMVC.Models;
//using RentAppMVC.Utilities;
//using RentAppMVC.BusinessLogicLayer;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

//namespace RentAppMVC.Controllers
//{
//    [Route("ShoppingCart")]
//    public class ShoppingCartController : Controller
//    {
//        private ShoppingCart _shoppingCart;
//        private readonly ProductCopyLogic _productCopyLogic;
//        private ProductLogic _productLogic;


//        public ShoppingCartController(ShoppingCart shoppingCart, ProductCopyLogic productCopyLogic, ProductLogic productLogic)
//        {
//            _shoppingCart = shoppingCart;
//            _productLogic = productLogic;
//            _productCopyLogic = productCopyLogic;

//        }

//        public IActionResult Index()
//        {
//            _shoppingCart = CookieUtility.ReadCart(HttpContext);
//            return View(_shoppingCart);
//        }

//        [HttpPost]
//        public async Task<ActionResult> AddToCart(int productId)
//        {
//            var shoppingCart = CookieUtility.ReadCart(HttpContext);

//            bool productAlreadyExists = shoppingCart.Items.Any(item => item.Product.ProductID == productId);

//            if (!productAlreadyExists)
//            {
//                if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
//                {
//                    shoppingCart.Product = await _productLogic.GetProductById(productId);
//                    CookieUtility.UpdateCart(HttpContext, shoppingCart);
//                    return RedirectToAction("Index", "Rent");
//                }

//                DateTime startDate = shoppingCart.StartDate;
//                DateTime endDate = shoppingCart.EndDate;
//                TimeSpan startTime = shoppingCart.StartTime;
//                TimeSpan endTime = shoppingCart.EndTime;

//                var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, startDate, endDate, startTime, endTime);
//                if (availableProductCopies == null || availableProductCopies.Count == 0)
//                {
//                    Product thisProduct= await _productLogic.GetProductById(productId);
//                    string productName = thisProduct.ProductName;
//                    TempData["ErrorMessage"] = $"The product '{productName}' is not available for the selected date and time.";
//                    return RedirectToAction("Index", "ShoppingCart");
//                }

//                var firstProductCopy = availableProductCopies[0];
//                Product product = await _productLogic.GetProductById(productId);
//                OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber, product);
//                shoppingCart.Items.Add(orderLine);

//                CookieUtility.UpdateCart(HttpContext, shoppingCart);
//            }
//            else
//            {
//                TempData["ErrorMessage"] = "This product is already in your cart.";
//            }

//            return RedirectToAction("Index");
//        }

//        [HttpPost("RemoveItem")]
//        public ActionResult RemoveItem(string serialNumber)
//        {
//            _shoppingCart = CookieUtility.ReadCart(HttpContext);
//            _shoppingCart.RemoveItem(serialNumber);
//            CookieUtility.UpdateCart(HttpContext, _shoppingCart);
//            return RedirectToAction("Index");
//        }

//        [HttpPost("EmptyCart")]
//        public ActionResult EmptyCart()
//        {
//            CookieUtility.EmptyCart(HttpContext);
//            return RedirectToAction("Index");
//        }
//    }
//}






using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Identity;

namespace RentAppMVC.Controllers
{
    [Route("ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartLogic _shoppingCartLogic;
        private ShoppingCart _shoppingCart;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PrivateCustomerLogic _privateCustomerLogic;
        private readonly BusinessCustomerLogic _businessCustomerLogic;

        public ShoppingCartController(ShoppingCart shoppingCart, ShoppingCartLogic shoppingCartLogic, UserManager<IdentityUser> userManager, PrivateCustomerLogic privateCustomerLogic, BusinessCustomerLogic businessCustomerLogic)
        {
            _shoppingCart = shoppingCart;
            _shoppingCartLogic = shoppingCartLogic;
            _userManager = userManager;
            _privateCustomerLogic = privateCustomerLogic;
            _businessCustomerLogic = businessCustomerLogic;
        }

        public IActionResult Index()
        {
            _shoppingCart = CookieUtility.ReadCart(HttpContext);
            return View(_shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            string userId = _userManager.GetUserId(User);
            bool isPrivateCustomer = await _privateCustomerLogic.IsPrivateCustomer(userId);
            bool isBusinessCustomer = await _businessCustomerLogic.IsBusinessCustomer(userId);


            if (!isPrivateCustomer && !isBusinessCustomer)
            {
                TempData["ErrorMessage"] = "You need to register as a customer first.";
                return RedirectToAction("Index", "Profile");
            }

            try
            {
                var updatedCart = await _shoppingCartLogic.AddToCart(HttpContext, productId);
                if (updatedCart == null)
                {
                    return RedirectToAction("Index", "Rent");
                }
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost("RemoveItem")]
        public IActionResult RemoveItem(string serialNumber)
        {
            _shoppingCartLogic.RemoveItem(HttpContext, serialNumber);
            return RedirectToAction("Index");
        }

        [HttpPost("EmptyCart")]
        public IActionResult EmptyCart()
        {
            _shoppingCartLogic.EmptyCart(HttpContext);
            return RedirectToAction("Index");
        }
    }
}
