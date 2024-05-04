using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public class ProductAccess : IProductAccess
    {
        private readonly IServiceConnection _productService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/Product/";

        public ProductAccess()
        {
            _productService = new ServiceConnection(_serviceBaseUrl);
        }
      
        public async Task<List<Product>?> GetAllProducts()
        {
            List<Product>? products = new List<Product>();

            HttpResponseMessage? response = await _productService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            }

            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            // Construct the URL with the correct format for retrieving a product by ID
            Product product = new Product();
            HttpResponseMessage? response = await _productService.GetById(productId);
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(jsonString);

            }
            return product;

        }



        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            List<Product> products = new List<Product>();

            HttpResponseMessage? response = await _productService.CallServiceGet(); // Call without any arguments
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            }

            // Filter products by categoryId
            products = products.Where(p => p.CategoryID == categoryId).ToList();

            return products;
        }

    }
}
