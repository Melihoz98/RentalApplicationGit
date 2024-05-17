using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Expressions;
using AdminWinForm.Models;
using System.Net;




namespace AdminWinForm.ServiceLayer
{
    public class CategoryServiceAccess : ICategoryAccess
    {
        readonly IServiceConnection _categoryService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/Category";
        static readonly string authenType = "Bearer";
        public HttpStatusCode CurrentHttpStatusCode { get; set; }
        public CategoryServiceAccess()
        {
            _categoryService = new ServiceConnection(_serviceBaseUrl);
        }



        public async Task<List<Category>> GetCategories(string tokenToUse)
        {
            List<Category> categories = null;

            _categoryService.UseUrl = _categoryService.BaseUrl;

            // Add the Bearer token to the request header
            string bearerTokenValue = authenType + " " + tokenToUse;
            _categoryService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization"); // To avoid multiple Authorization headers
            _categoryService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

            try
            {
                HttpResponseMessage response = await _categoryService.CallServiceGet();
                CurrentHttpStatusCode = response != null ? response.StatusCode : HttpStatusCode.BadRequest;

                if (response != null && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(content);
                }
                else
                {
                    if (response != null && response.StatusCode == HttpStatusCode.NoContent)
                    {
                        categories = new List<Category>();
                    }
                    else
                    {
                        categories = null;
                    }
                }
            }
            catch
            {
                categories = null;
            }

            return categories;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            Category category = new Category();

            HttpResponseMessage? response = await _categoryService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
                category = categories.FirstOrDefault(p => p.CategoryID == categoryId);
            }

            return category;
        }


        public async Task<int> AddCategory(Category categoryToAdd)
            {
                int insertedCategoryID = -1;
                _categoryService.UseUrl = _categoryService.UseUrl;
                try
                {
                    string categoryJson = JsonConvert.SerializeObject(categoryToAdd);
                    var httpContent = new StringContent(JsonConvert.SerializeObject(categoryToAdd), Encoding.UTF8, "application/json");
                    var serviceResponse = await _categoryService.CallServicePost(httpContent);

                    if (serviceResponse is not null && serviceResponse.IsSuccessStatusCode)
                    {
                        string idString = await serviceResponse.Content.ReadAsStringAsync();
                        bool idNumOk = int.TryParse(idString, out insertedCategoryID);
                        if (idNumOk)
                        {
                            insertedCategoryID = -2;
                        }
                    }
                } catch
                {
                    insertedCategoryID = -3;
                }
                return insertedCategoryID;

            }

          

            public async Task<bool> DeleteCategory(int categoryId)
            {
                _categoryService.UseUrl += $"{categoryId}";

                HttpResponseMessage? response = await _categoryService.CallServiceDelete();
                return response != null && response.IsSuccessStatusCode;
            }
        
    }
}


    

