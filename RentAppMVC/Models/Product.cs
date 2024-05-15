namespace RentAppMVC.Models
{
    public class Product
    {
        public Product() { }

        public Product(int productID, string? productName, string? description, decimal hourlyPrice, int? categoryID, string? imagePath)
        {
            ProductID = productID;
            ProductName = productName;
            Description = description;
            HourlyPrice = hourlyPrice;
            CategoryID = categoryID;
            ImagePath = imagePath;

        }

        public Product(string? productName, string? description, decimal hourlyPrice, int? categoryID, string? imagePath)
        {
            ProductName = productName;
            Description = description;
            HourlyPrice = hourlyPrice;
            CategoryID = categoryID;
            ImagePath = imagePath;
        }



        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal HourlyPrice { get; set; }
        public int? CategoryID { get; set; }
        public string ImagePath { get; set; }
    }
}