﻿namespace RentAppMVC.Models
{
    public class OrderLine
    {

        public OrderLine(int orderID, string serialNumber, Product product)
        {
            OrderID = orderID;
            SerialNumber = serialNumber;
            Product = product;
        }

        public int OrderID { get; set; }
        public string SerialNumber { get; set; }
        public Product Product { get; set; }    
    }

}
