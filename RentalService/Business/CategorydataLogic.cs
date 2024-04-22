using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using System;
namespace RentalService.Business
{
    public class CategorydataLogic : ICategoryData
    {
        private readonly ICategoryAccess _categoryAccess;

        public CategorydataLogic(ICategoryAccess inCategoryAccess)
        {
            _categoryAccess = inCategoryAccess;

        }

        public CategoryDto? GetByID(int idToMatch)
        {
            CategoryDto? foundCategoryDto;
            try
            {
                Category? foundCategory = _categoryAccess.GetCategoryById(idToMatch);
                foundCategoryDto = ModelConversion.CategoryDtoConvert.FromCategory(foundCategory);
            }
            catch
            {
                foundCategoryDto = null;
            }
            return foundCategoryDto;
        }


        public List<CategoryDto?>? GetAllCategories() 
        {
            List<CategoryDto?>? foundDtos;
            try
            {
                List<Category>? foundCategories = _categoryAccess.GetCategoryAll();
                foundDtos = ModelConversion.CategoryDtoConvert.FromCategoryCollection(foundCategories);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string xx = ex.Message;
            }
            return foundDtos;

        }
    }
}
