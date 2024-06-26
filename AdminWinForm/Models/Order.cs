﻿namespace AdminWinForm.Models
{
    public class Order
    {
        public Order() { }

        public Order(int orderID, string customerID, DateTime orderDate, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, int totalHours, decimal subTotalPrice, decimal totalOrderPrice)
        {
            OrderID = orderID;
            CustomerID = customerID;
            OrderDate = orderDate;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            TotalHours = totalHours;
            SubTotalPrice = subTotalPrice;
            TotalOrderPrice = totalOrderPrice;
        }

        public Order(string customerID, DateTime orderDate, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, int totalHours, decimal subTotalPrice, decimal totalOrderPrice)
        {
            CustomerID = customerID;
            OrderDate = orderDate;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            TotalHours = totalHours;
            SubTotalPrice = subTotalPrice;
            TotalOrderPrice = totalOrderPrice;
        }

        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TotalHours { get; set; }
        public decimal SubTotalPrice { get; set; }
        public decimal TotalOrderPrice { get; set; }
    }

}
