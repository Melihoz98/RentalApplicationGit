using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;


namespace RentAppMVC.Controllers
{
    [Route("Rent")]
    public class RentController : Controller
    {
        private readonly ShoppingCartLogic _shoppingCartLogic;

        public RentController(ShoppingCartLogic shoppingCartLogic)
        {
            _shoppingCartLogic = shoppingCartLogic;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var shoppingCart = _shoppingCartLogic.ReadCartFromCookie(HttpContext);
            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmDateTime(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                var updatedCart = await _shoppingCartLogic.ConfirmDateTime(HttpContext, startDate, endDate, startTime, endTime);
                return RedirectToAction("Index", "ShoppingCart");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
