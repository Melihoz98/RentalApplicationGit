using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface ICategoryAccess
    {
        Task<List<Category>?>? GetCategories();
        Task<Category> GetCategoryById(int categoryId);
    }
}
