using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public class CategoryServiceAccess : ICategoryAccess
    {
        readonly IServiceConnection _categoryService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/Categories/";

        public CategoryServiceAccess()
        {
            _categoryService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<Category>?> GetCategories()
        {
            List<Category>? categories = null;

            HttpResponseMessage? response = await _categoryService.CallServiceGet(_serviceBaseUrl); // Use full base URL for GetCategories

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

            string url = $"{_serviceBaseUrl}/{categoryId}";

            HttpResponseMessage? response = await _categoryService.CallServiceGet(url);  // Use specific URL with categoryId

            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<Category>(jsonString);
            }

            return category;
        }
    }
}
