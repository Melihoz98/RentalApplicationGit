using RentalService.DTO;

namespace RentalService.Business
{
    public interface ICategoryData
    {
        CategoryDto? GetByID(int id);
        List<CategoryDto?>? GetAllCategories();
    }
}
