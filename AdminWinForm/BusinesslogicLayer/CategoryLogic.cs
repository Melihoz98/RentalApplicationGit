﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;


namespace AdminWinForm.BusinesslogicLayer
{
    public class CategoryLogic
    {
        readonly ICategoryAccess _categoryAccess;

        public CategoryLogic()
        {
            _categoryAccess = new CategoryServiceAccess();
        }
        public async Task<List<Category>?> GetAllCategories( )
        {
            List<Category>? foundCategories = null;
            if(_categoryAccess != null )
            {
                foundCategories = await _categoryAccess.GetCategories();

            }
            return foundCategories;
        }

        public async Task<int> AddCategory( string categoryName, string imagePath)
        {
            Category newCategory = new Category(categoryName, imagePath);
            int insertedCategoryId = await _categoryAccess.AddCategory(newCategory);
             return insertedCategoryId;   
        }

        public async Task<bool> UpdateCategory(int categoryID, string categoryName, string imagePath)
        {
            Category existingCategory = await _categoryAccess.GetCategoryById(categoryID);
             if(existingCategory != null)
            {
                existingCategory.CategoryID = categoryID;
                existingCategory.CategoryName = categoryName;
                existingCategory.ImagePath = imagePath;

                return await _categoryAccess.UpdateCategory(existingCategory);

            }   
             return false;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            return await _categoryAccess.DeleteCategory(categoryId);
        }
    }
}
