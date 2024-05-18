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
        readonly string _serviceBaseUrl = "https://localhost:7023/api/Category/";
        static readonly string authenType = "Bearer";
        public HttpStatusCode CurrentHttpStatusCode { get; set; }
        public CategoryServiceAccess()
        {
            _categoryService = new ServiceConnection(_serviceBaseUrl);
        }
 

            public async Task<List<Category>> GetCategories()
            {
                List<Category> categories = new List<Category>();
                HttpResponseMessage? response = await _categoryService.CallServiceGet();
                if (response != null && response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
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


        public async Task<int> AddCategory(string tokenToUse, Category categoryToAdd)
        {
            int insertedCategoryID = -1;
            _categoryService.UseUrl = _categoryService.BaseUrl;

            // Must add Bearer token to request header
            string bearerTokenValue = authenType + " " + tokenToUse;
            _categoryService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization"); // To avoid more Authorization headers
            _categoryService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

            try
            {
                string categoryJson = JsonConvert.SerializeObject(categoryToAdd);
                var httpContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
                var serviceResponse = await _categoryService.CallServicePost(httpContent);

                CurrentHttpStatusCode = serviceResponse != null ? serviceResponse.StatusCode : HttpStatusCode.BadRequest; // Used by logic class

                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    string idString = await serviceResponse.Content.ReadAsStringAsync();
                    bool idNumOk = int.TryParse(idString, out insertedCategoryID);
                    if (!idNumOk)
                    {
                        insertedCategoryID = -4; // Adjusted to align with other method responses
                    }
                }
                else
                {
                    insertedCategoryID = -2; // Adjusted to align with other method responses
                }
            }
            catch
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


    

