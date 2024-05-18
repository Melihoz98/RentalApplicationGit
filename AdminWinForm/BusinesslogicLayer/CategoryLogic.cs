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
        readonly ICategoryAccess _categoryAccess;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }
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

        public async Task<int> AddCategory(string categoryName, string imagePath)
        {
            int insertedCategoryId = -1;
            Category newCategory = new Category(categoryName, imagePath);
            

            // Get token
            TokenState currentState = TokenState.Valid; // Presumed state
            string? tokenValue = await GetToken(currentState);
            if (tokenValue != null)
            {
                insertedCategoryId = await _categoryAccess.AddCategory(tokenValue, newCategory);
               
                if (_categoryAccess.CurrentHttpStatusCode == HttpStatusCode.Unauthorized)
                {
                    currentState = TokenState.Invalid;
                    insertedCategoryId = -2;
                }
            }
            else
            {
                currentState = TokenState.Invalid;
                tokenValue = await GetToken(currentState);


                if (tokenValue != null)
                {
                    insertedCategoryId = await _categoryAccess.AddCategory(tokenValue, newCategory);

                }
            }

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
