using Newtonsoft.Json;
using RentAppMVC.Models;

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

        public static void AddDatesAndTimesToCart(HttpContext httpContext, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            ShoppingCart cart = ReadCart(httpContext);
            cart.StartDate = startDate;
            cart.EndDate = endDate;
            cart.StartTime = startTime;
            cart.EndTime = endTime;
            UpdateCart(httpContext, cart);
        }

        public static string GetCookieValue(HttpContext httpContext, string shoopingCartCookie)
        {
            if (httpContext.Request.Cookies.TryGetValue(shoopingCartCookie, out string cookieValue))
            {
                return cookieValue;
            }
            else
            {
                return null; 
            }
        }

        public static void RemoveItem(HttpContext httpContext, string serialNumber)
        {
            ShoppingCart cart = ReadCart(httpContext);
            cart.RemoveItem(serialNumber);
            UpdateCart(httpContext, cart);
        }

    }
}
