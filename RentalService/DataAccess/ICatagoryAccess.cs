using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface ICategoryAccess
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        int AddCategory(Category category);
       
        void DeleteCategory(int categoryId);
    }
}
