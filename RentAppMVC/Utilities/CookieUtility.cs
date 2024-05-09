using Newtonsoft.Json;
using RentAppMVC.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace RentAppMVC.Utilities
{
    public static class CookieUtility
    {
        public static ShoppingCart ReadCart(HttpContext httpContext)
        {
            ShoppingCart cart = new ShoppingCart();
            if (httpContext.Request.Cookies.ContainsKey("shoppingCart"))
            {
                string cookieDataJson = httpContext.Request.Cookies["shoppingCart"];
                cart = JsonConvert.DeserializeObject<ShoppingCart>(cookieDataJson);
            }
            return cart;
        }

        public static void UpdateCart(HttpContext httpContext, ShoppingCart cart)
        {
            string cartJson = JsonConvert.SerializeObject(cart);
            httpContext.Response.Cookies.Append("shoppingCart", cartJson);
        }

        public static void EmptyCart(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("shoppingCart");
        }
    }
}
