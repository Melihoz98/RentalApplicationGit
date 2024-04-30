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

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Retrieve existing cart from session or create new cart
            List<int> cart = HttpContext.Session.Get<List<int>>("Cart") ?? new List<int>();

            // Add productId to cart
            cart.Add(productId);

            // Update cart in session
            HttpContext.Session.Set("Cart", cart);

            // Redirect back to the product list or wherever appropriate
            return RedirectToAction("Index");
        }
    }
}
