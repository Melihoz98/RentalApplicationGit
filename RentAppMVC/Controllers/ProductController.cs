using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;

namespace RentAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductLogic _productLogic;

        public ProductController(ProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductsByCategory(int categoryId)
        {
            List<Product> products = await _productLogic.GetProductsByCategoryId(categoryId);
            return View(products);
        }
    }
}
