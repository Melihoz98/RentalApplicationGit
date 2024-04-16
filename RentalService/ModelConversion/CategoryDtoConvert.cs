using RentalService.Models;
using RentalService.DTO;
using System;

namespace RentalService.ModelConversion
{
    public class CategoryDtoConvert
    {

        public static List<CategoryDto?>? FromCategoryCollection(List<Category> inCategories)
        {
            List<CategoryDto?>? aCategoryReadDtoList = null;
            if (inCategories != null)
            {
                aCategoryReadDtoList = new List<CategoryDto?>();
                CategoryDto? tempDto;
                foreach (Category aCategory in inCategories)
                {
                    if (aCategory != null)
                    {
                        tempDto = FromCategory(aCategory);
                        aCategoryReadDtoList.Add(tempDto);
                    }
                }
            }
            return aCategoryReadDtoList;
        }



        public static CategoryDto? FromCategory(Category inCategory)
        {
            CategoryDto? aCategoryReadDto = null;
            if (inCategory != null)
            {
                aCategoryReadDto = new CategoryDto(inCategory.CategoryName);
            }
            return aCategoryReadDto;
        }






        // Convert from PersonDTO object to Person object
        public static Category? ToCategory(CategoryDto inDto)
        {
            Category? aCategory = null;
            if (inDto != null)
            {
                aCategory = new Category(inDto.CategoryName);
            }
            return aCategory;
        }




    }
}
