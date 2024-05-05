using Microsoft.AspNetCore.Mvc;

namespace YourAppName.Controllers
{
    [Route("Rent")]
    public class RentController : Controller
    {
        // GET: Rent/Index
        public IActionResult Index()
        {
            // You can pass any necessary data to the view here
            return View();
        }
    }
}
