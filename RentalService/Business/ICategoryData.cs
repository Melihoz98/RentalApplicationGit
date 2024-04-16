using RentalService.DTO;

namespace RentalService.Business
{
    public interface ICategoryData
    {
        CategoryDto? Get(int id);
        List<CategoryDto?>? Get();
    }
}
