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
        return View("Index", _shoppingCart); 
    }

    [HttpPost]
    public async Task<ActionResult> AddItem(int productId)
    {
        if (!_shoppingCart.IsEmpty())
        {
            var productCopies = await _productCopyLogic.GetAllProductCopyByID(productId);
            if (productCopies != null && productCopies.Count > 0)
            {
                foreach (var productCopy in productCopies)
                {
                    OrderLine orderLine = new OrderLine(-1, productCopy.SerialNumber);

                    _shoppingCart.AddItem(orderLine);
                }
            }
            return RedirectToAction("Index");
        }
        else
        {
            var productCopies = await _productCopyLogic.GetAllProductCopyByID(productId);
            if (productCopies != null && productCopies.Count > 0)
            {
                var serialNumbers = productCopies.Select(pc => pc.SerialNumber).ToList();
                var orderLines = serialNumbers.Select(sn => new OrderLine(-1, sn)).ToList();
                foreach (var orderLine in orderLines)
                {
                    _shoppingCart.AddItem(orderLine);
                }
            }
            return RedirectToAction("Index", "Rent");
        }
    }

    [HttpPost]
    public ActionResult RemoveItem(string serialNumber)
    {
        _shoppingCart.RemoveItem(serialNumber);
        return RedirectToAction("Index");
    }
}
