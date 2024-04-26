﻿namespace AdminWinForm.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Product(string productName, string description, decimal hourlyPrice, int categoryID, string imagePath)
        {
            ProductName = productName;
            Description = description;
            HourlyPrice = hourlyPrice;
            CategoryID = categoryID;

        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal HourlyPrice { get; set; }
        public int CategoryID { get; set; }
        public string ImagePath { get; set; }
    }

}
