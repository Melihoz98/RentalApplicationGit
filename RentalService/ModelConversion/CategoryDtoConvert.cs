using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class CategoryDtoConvert
    {
       
        public static List<CategoryDto?>? FromCategoryCollection(List<Category> inCategories)
        {
            List<CategoryDto?>? categoryDtoList = null;
            if (inCategories != null)
            {
                categoryDtoList = new List<CategoryDto?>();
                foreach (Category category in inCategories)
                {
                    if (category != null)
                    {
                        CategoryDto? dto = FromCategory(category);
                        categoryDtoList.Add(dto);
                    }
                }
            }
            return categoryDtoList;
        }

        
        public static CategoryDto? FromCategory(Category category)
        {
            CategoryDto? categoryDto = null;
            if (category != null)
            {
                categoryDto = new CategoryDto(category.CategoryName, category.ImagePath)
                {
                    CategoryID = category.CategoryID
                };
            }
            return categoryDto;
        }

        
        public static Category? ToCategory(CategoryDto dto)
        {
            Category? category = null;
            if (dto != null)
            {
                category = new Category(dto.CategoryID, dto.CategoryName, dto.ImagePath);
            }
            return category;
        }
    }
}
