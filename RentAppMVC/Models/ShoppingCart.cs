using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RentAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentAppMVC.Models
{
    public class ShoppingCart
    {
        public class ShoppingCartData
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public List<OrderLine> Items { get; set; }
        }

        private readonly List<OrderLine> _items;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCart(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var cartData = GetShoppingCartDataFromCookie();
            _items = cartData?.Items ?? new List<OrderLine>();
        }

        private void SaveCartToCookie()
        {
            var cartData = new ShoppingCartData { Items = _items };
            var cartJson = JsonConvert.SerializeObject(cartData);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("shoppingCartCookie", cartJson);
        }

        private ShoppingCartData GetShoppingCartDataFromCookie()
        {
            string cartJson = _httpContextAccessor.HttpContext.Request.Cookies["shoppingCartCookie"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                return JsonConvert.DeserializeObject<ShoppingCartData>(cartJson);
            }
            return null;
        }

        public void AddItem(OrderLine orderLine)
        {
            _items.Add(orderLine);
            SaveCartToCookie();
        }

        public void RemoveItem(string serialNumber)
        {
            OrderLine orderLineToRemove = _items.FirstOrDefault(ol => ol.SerialNumber == serialNumber);
            if (orderLineToRemove != null)
            {
                _items.Remove(orderLineToRemove);
                SaveCartToCookie();
            }
        }

        public List<OrderLine> GetItems()
        {
            return _items;
        }

        public bool IsEmpty()
        {
            return _items.Count == 0;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}



