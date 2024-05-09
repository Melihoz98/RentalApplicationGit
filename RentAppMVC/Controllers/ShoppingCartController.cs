using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
using System.Threading.Tasks;

namespace RentAppMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ProductCopyLogic _productCopyLogic;

        public ShoppingCartController(ShoppingCart shoppingCart, ProductCopyLogic productCopyLogic)
        {
            _shoppingCart = shoppingCart;
            _productCopyLogic = productCopyLogic;
        }

        public IActionResult Index()
        {
            return View(_shoppingCart); 
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {
            var productCopies = await _productCopyLogic.GetAllProductCopyByID(productId);
            if (productCopies == null || productCopies.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var cart = new ShoppingCart();

            foreach (var productCopy in productCopies)
            {
                OrderLine orderLine = new OrderLine(-1, productCopy.SerialNumber);
                cart.OrderLines.Add(orderLine);
            }

            if (_shoppingCart.IsEmpty())
            {
                CookieUtility.UpdateCart(HttpContext, cart);

                return RedirectToAction("Index", "Rent");
            }
            else
            {
                _shoppingCart.OrderLines.AddRange(cart.OrderLines);

                CookieUtility.UpdateCart(HttpContext, _shoppingCart);

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult RemoveItem(string serialNumber)
        {
            _shoppingCart.RemoveItem(serialNumber); 
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EmptyCart()
        {
            _shoppingCart.OrderLines.Clear(); 
            return RedirectToAction("Index");
        }
    }
}
