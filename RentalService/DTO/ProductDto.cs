namespace RentalService.DTO
{
    public class ProductDto
    {
        public ProductDto() { }

        public ProductDto(string productName, string description, decimal hourlyPrice, int categoryID, string imagePath)
        {
            ProductName = productName;
            Description = description;
            HourlyPrice = hourlyPrice;
            CategoryID = categoryID;
            ImagePath = imagePath;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal HourlyPrice { get; set; }
        public int CategoryID { get; set; }
        public string ImagePath { get; set; }
    }
}
