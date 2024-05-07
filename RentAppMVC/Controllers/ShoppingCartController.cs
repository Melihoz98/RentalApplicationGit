using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;

public class ShoppingCartController : Controller
{
    private readonly ShoppingCart _shoppingCart;
    private readonly ProductLogic _productLogic;
    private readonly ILogger<ShoppingCartController> _logger; // Tilføj logger

    // Opdater constructor for at inkludere ILogger
    public ShoppingCartController(ShoppingCart shoppingCart, ProductLogic productLogic, ILogger<ShoppingCartController> logger)
    {
        _shoppingCart = shoppingCart;
        _productLogic = productLogic;
        _logger = logger; // Initialiser logger
    }

    public IActionResult Index()
    {
        var items = _shoppingCart.GetItems();
        if (items == null)
        {
            items = new List<Product>(); // Lav en tom liste, hvis der ikke er nogen elementer
        }
        return View(items);
    }


    [HttpPost]
    public async Task<ActionResult> AddItem(int productId)
    {
        // Check if the shopping cart is empty
        if (_shoppingCart.IsEmpty())
        {
            // If the shopping cart is empty, store the product ID in session and redirect to choose date and time action
            HttpContext.Session.SetInt32("productId", productId);
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
        // Here you can return a partial view to choose date and time
        return PartialView("_ChooseDateTime");
    }

    [HttpPost]
    public async Task<ActionResult> ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            // Check if there is a product to add to the cart
            if (HttpContext.Session.TryGetValue("productId", out byte[] productIdBytes))
            {
                int productId = BitConverter.ToInt32(productIdBytes, 0);

                // Add the product to the cart
                await _shoppingCart.AddItem(productId, _productLogic);

                // Optionally, set the chosen date and time in the shopping cart
                _shoppingCart.SetDateTime(startDate, endDate, startTime, endTime);

                // Clear the productId from session
                HttpContext.Session.Remove("productId");

                // Redirect to Index action
                return RedirectToAction("Index");
            }
            else
            {
                // If there is no product to add, redirect to some default action or display an error message
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error confirming date and time");
            throw; // You might handle this differently depending on your requirements
        }
    }


}


