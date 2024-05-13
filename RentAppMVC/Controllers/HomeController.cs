using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RentAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryLogic _categoryLogic;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _categoryLogic = new CategoryLogic();
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryLogic.GetAllCategories();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoriesPartial()
        {
            var categories = await _categoryLogic.GetAllCategories();
            return PartialView("_CategoriesPartial", categories);
        }

       
        [HttpPost]
        public IActionResult SelectCategory(int categoryId)
        {
            
            return RedirectToAction("ProductsByCategory", "Product", new { categoryId });
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
