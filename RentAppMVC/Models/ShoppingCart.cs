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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        public ShoppingCart()
        {
            OrderLines = new List<OrderLine>();
        }

        public bool IsEmpty()
        {
            return OrderLines.Count == 0;
        }

        public void RemoveItem(string serialNumber)
        {
            OrderLine itemToRemove = OrderLines.FirstOrDefault(o => o.SerialNumber == serialNumber);
            if (itemToRemove != null)
            {
                OrderLines.Remove(itemToRemove);
            }
        }
    }
}



