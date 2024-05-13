using Microsoft.AspNetCore.Mvc;

namespace YourAppName.Controllers
{
    [Route("Rent")]
    public class RentController : Controller
    {
     
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
