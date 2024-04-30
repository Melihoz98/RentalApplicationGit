using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class CategoryLogic
    {
        private readonly ICategoryAccess _categoryAccess;

        public CategoryLogic()
        {
            _categoryAccess = new CategoryAccess();
        }

        public async Task<List<Category>?> GetAllCategories()
        {
            List<Category>? foundCategories = null;
            if (_categoryAccess != null)
            {
                foundCategories = await _categoryAccess.GetCategories();
            }
            return foundCategories;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _categoryAccess.GetCategoryById(categoryId);
        }
    }
}
