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
            var itemToRemove = Items.FirstOrDefault(i => i.SerialNumber == serialNumber);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        public int TotalHours
        {
            get
            {
                TimeSpan timeDifference = (EndDate.Date + EndTime) - (StartDate.Date + StartTime);
                return (int)timeDifference.TotalHours;
            }
        }

        public decimal SubTotalPrice
        {
            get
            {
                decimal subTotal = 0;
                foreach (var orderLine in Items)
                {
                    subTotal += orderLine.Product.HourlyPrice;
                }
                return subTotal;
            }
        }

        public decimal TotalOrderPrice
        {
            get
            {
                return SubTotalPrice * TotalHours;
            }
        }
    }
}



