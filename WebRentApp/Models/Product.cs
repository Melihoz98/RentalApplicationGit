﻿namespace RentalService.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal HourlyPrice { get; set; }
        public int Inventory { get; set; }
        public int CategoryID { get; set; }
    }

}