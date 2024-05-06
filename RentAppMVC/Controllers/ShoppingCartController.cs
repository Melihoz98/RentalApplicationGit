using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;

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
        var items = _shoppingCart.GetItems();
        return View(items);
    }

    [HttpPost]
    public async Task<ActionResult> AddItem(int productId)
    {
        // Retrieve product copies by ProductID from API
        var productCopies = await _productCopyLogic.GetAllProductCopyByID(productId);
        if (productCopies != null && productCopies.Count > 0)
        {
            foreach (var productCopy in productCopies)
            {
                _shoppingCart.AddItem(productCopy);
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult RemoveItem(string serialNumber)
    {
        _shoppingCart.RemoveItem(serialNumber);
        return RedirectToAction("Index");
    }
}
