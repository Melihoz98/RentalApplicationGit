using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface ICategoryData
    {
        CategoryDto? GetById(int idToMatch);
        List<CategoryDto?>? GetAllCategories();
        int CreateCategory(CategoryDto categoryDto);
        
        void DeleteCategory(int id);
    }
}
