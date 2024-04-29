using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public class CategoryAccess : ICategoryAccess
    {
        private readonly IServiceConnection _categoryService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/Category/";

        public CategoryAccess()
        {
            _categoryService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<Category>?> GetCategories()
        {
            List<Category>? categories = null;

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

            HttpResponseMessage? response = await _categoryService.GetById(categoryId.ToString());
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<Category>(jsonString);
            }

            return category;
        }
    }
}
