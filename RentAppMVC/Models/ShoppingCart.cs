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
        public List<OrderLine> Items { get; set; }
        public Product Product { get; set; }

        public ShoppingCart()
        {
            Items = new List<OrderLine>();
        }

        public bool IsEmpty()
        {
            return Items.Count == 0;
        }

        public void RemoveItem(string serialNumber)
        {
            OrderLine itemToRemove = Items.FirstOrDefault(o => o.SerialNumber == serialNumber);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }
    }
}



