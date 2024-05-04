using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;

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
        await _shoppingCart.AddItem(productId, _productLogic);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult RemoveItem(int productId)
    {
        _shoppingCart.RemoveItem(productId);
        return RedirectToAction("Index");
    }
}
