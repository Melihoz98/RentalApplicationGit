using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;

namespace RentAppMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ProductLogic _productLogic;

        public CartController(ShoppingCart shoppingCart, ProductLogic productLogic)
        {
            _shoppingCart = shoppingCart;
            _productLogic = productLogic;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = _shoppingCart.GetItems();
            return View(cartItems);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {
            // Retrieve the product based on the productId using ProductLogic
            var product = await _productLogic.GetProductById(productId);

            // Add the product to the shopping cart
            _shoppingCart.AddItem(product);

            // Redirect the user back to the cart index page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            // Remove the item with the specified productId from the shopping cart
            _shoppingCart.RemoveItem(productId);

            // Redirect the user back to the cart index page
            return RedirectToAction("Index");
        }
    }
}
