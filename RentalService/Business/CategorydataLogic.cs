using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class CategoryDataLogic : ICategoryData
    {
        private readonly ICategoryAccess _categoryAccess;

        public CategoryDataLogic(ICategoryAccess categoryAccess)
        {
            _categoryAccess = categoryAccess;
        }

        public CategoryDto? GetById(int idToMatch)
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
                List<Category>? foundCategories = _categoryAccess.GetCategories();
                foundDtos = ModelConversion.CategoryDtoConvert.FromCategoryCollection(foundCategories);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string errorMessage = ex.Message;
                
            }
            return foundDtos;
        }

        public int CreateCategory(CategoryDto categoryDto)
        {
            int insertedId = 0;
            try
            {
                Category? dbCategory = ModelConversion.CategoryDtoConvert.ToCategory(categoryDto);
                if (dbCategory != null)
                {
                    insertedId = _categoryAccess.AddCategory(dbCategory);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                
            }
            return insertedId;
        }

      

        public void DeleteCategory(int id)
        {
            try
            {
                _categoryAccess.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                
            }
        }
    }
}
