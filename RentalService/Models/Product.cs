namespace RentalService.Models
{
    public class Product
    {
        public Product(){}

        public Product(int productID, string? productName, string? description, decimal? hourlyPrice, int? inventory, int? categoryID)
        {
            ProductID = productID;
            ProductName = productName;
            Description = description;
            HourlyPrice = hourlyPrice;
            Inventory = inventory;
            CategoryID = categoryID;
        }

        public Product(string? productName, string? description, decimal? hourlyPrice, int? inventory, int? categoryID)
        {
            ProductName = productName;
            Description = description;
            HourlyPrice = hourlyPrice;
            Inventory = inventory;
            CategoryID = categoryID;
        }



        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal? HourlyPrice { get; set; }
        public int? Inventory { get; set; }
        public int? CategoryID { get; set; }
    }

}
