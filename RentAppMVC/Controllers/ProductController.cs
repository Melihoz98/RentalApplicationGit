using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace RentAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductLogic _productLogic;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            _productLogic = new ProductLogic();
        }

        public async Task<IActionResult> Index(int categoryId)
        {
            List<Product> products = await _productLogic.GetProductsByCategoryId(categoryId);
            return View(products);
        }

    }
}
