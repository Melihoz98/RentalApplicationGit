namespace RentalService.DTO
{
    public class CategoryDto
    {
        public CategoryDto() { }

        public CategoryDto( string categoryName, string imagePath)
        {
            CategoryName = categoryName;
            ImagePath = imagePath;
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
    }
}
