using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
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
