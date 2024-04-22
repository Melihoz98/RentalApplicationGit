using RentalService.Models;
using System;
namespace RentalService.DataAccess
{
    public interface ICategoryAccess
    {
        Category GetCategoryById(int id);
        List<Category> GetCategoryAll();



    }
}
