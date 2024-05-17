using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.Security;
using AdminWinForm.ServiceLayer;


namespace AdminWinForm.BusinesslogicLayer
{
    public class CategoryLogic
    {
        public HttpStatusCode CurrentHttpStatusCode { get; set; }
        readonly ICategoryAccess _categoryAccess;

        public CategoryLogic()
        {
            _categoryAccess = new CategoryServiceAccess();
        }
        public async Task<List<Category>?> GetAllCategories()
        {
            List<Category>? foundCategories = null;

            // Get token
            TokenState currentState = TokenState.Valid; // Presumed state
            string? tokenValue = await GetToken(currentState);
            if (tokenValue != null)
            {
                foundCategories = await _categoryAccess.GetCategories(tokenValue);
                if (_categoryAccess.CurrentHttpStatusCode == HttpStatusCode.Unauthorized)
                {
                    currentState = TokenState.Invalid;
                }
            }
            else
            {
                currentState = TokenState.Invalid;
            }

            if (currentState == TokenState.Invalid)
            {
                tokenValue = await GetToken(currentState);
                if (tokenValue != null)
                {
                    foundCategories = await _categoryAccess.GetCategories(tokenValue);
                }
            }

            return foundCategories;
        }

        public async Task<int> AddCategory( string categoryName, string imagePath)
        {
            Category newCategory = new Category(categoryName, imagePath);
            int insertedCategoryId = await _categoryAccess.AddCategory(newCategory);
             return insertedCategoryId;   
        }

        

        public async Task<bool> DeleteCategory(int categoryId)
        {
            return await _categoryAccess.DeleteCategory(categoryId);
        }
        private async Task<string?> GetToken(TokenState useState)
        {
            TokenManager tokenHelp = new TokenManager();
            string? foundToken = await tokenHelp.GetToken(useState);
            return foundToken;
        }

    }
}
