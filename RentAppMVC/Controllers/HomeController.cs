using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Diagnostics;

namespace RentAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var categoryLogic = new CategoryLogic();
            var categories = categoryLogic.GetAllCategories().Result;

            return View(categories);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            return View(loggedInUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
