using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RentAppMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve cart items from the cookie
            List<ShoppingCartItem> cartItems = GetCartItemsFromCookie();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Retrieve product details from database
            var product = GetProductById(productId);

            // Create a new shopping cart item
            var item = new ShoppingCartItem
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                Price = (decimal)product.HourlyPrice,
            };

            // Retrieve existing cart items from the cookie
            List<ShoppingCartItem> cartItems = GetCartItemsFromCookie();

            // Add the new item to the cart
            cartItems.Add(item);

            // Save the updated cart items to the cookie
            SaveCartItemsToCookie(cartItems);

            // Redirect to the shopping cart page
            return RedirectToAction("Index");
        }

        private Product GetProductById(int productId)
        {
            // Simulate retrieving product details from the database
            // You should replace this with your actual logic for retrieving products
            // For demonstration purposes, I'll create a dummy product
            return new Product
            {
                ProductID = productId,
                ProductName = "Product " + productId,
                HourlyPrice = (decimal?)10.00 // Dummy price
            };
        }

        private List<ShoppingCartItem> GetCartItemsFromCookie()
        {
            string cartCookieValue = Request.Cookies["ShoppingCart"];

            if (cartCookieValue != null && !string.IsNullOrEmpty(cartCookieValue))
            {
                // Deserialize the cookie value into a list of ShoppingCartItem objects
                var cartItems = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartCookieValue);
                return cartItems;
            }

            return new List<ShoppingCartItem>();
        }

        private void SaveCartItemsToCookie(List<ShoppingCartItem> cartItems)
        {
            // Serialize the cart items into JSON format
            string cartItemsJson = JsonConvert.SerializeObject(cartItems);

            // Create a new cookie and add the serialized cart items as its value
            Response.Cookies.Append("ShoppingCart", cartItemsJson, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            });
        }
    }
}
