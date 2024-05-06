using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;

public class ShoppingCartController : Controller
{
    private readonly ShoppingCart _shoppingCart;
    private readonly ProductLogic _productLogic;

    public ShoppingCartController(ShoppingCart shoppingCart, ProductLogic productLogic)
    {
        _shoppingCart = shoppingCart;
        _productLogic = productLogic;
    }

    public IActionResult Index()
    {
        var items = _shoppingCart.GetItems();
        return View(items);
    }

    [HttpPost]
    public async Task<ActionResult> AddItem(int productId)
    {
        if (_shoppingCart.IsEmpty())
        {
            // If the shopping cart is empty, redirect to choose date and time page
            return RedirectToAction("ChooseDateTime");
        }
        else
        {
            // If the shopping cart is not empty, add the product to the cart
            await _shoppingCart.AddItem(productId, _productLogic);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public ActionResult RemoveItem(int productId)
    {
        _shoppingCart.RemoveItem(productId);
        return RedirectToAction("Index");
    }

    public IActionResult ChooseDateTime()
    {
        // Here you can return a view to choose date and time
        return View();
    }
}
