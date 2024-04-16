namespace RentalService.DTO
{
    public class CategoryDto
    {
        public CategoryDto() { 
        }

        public CategoryDto(string? categoryName)
        {
            CategoryName = categoryName;
        }

        public string? CategoryName { get; set;}
    }
}
