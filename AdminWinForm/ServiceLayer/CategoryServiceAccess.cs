﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Expressions;
using AdminWinForm.Models;




namespace AdminWinForm.ServiceLayer
{
    public class CategoryServiceAccess : ICategoryAccess
    {
        readonly IServiceConnection _categoryService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/Category/";

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


    

