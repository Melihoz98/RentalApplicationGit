using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdminWinForm.Models;
using System.Linq.Expressions;

namespace AdminWinForm.ServiceLayer
{
    public class ProductServiceAccess : IProductAccess
    {

        readonly IServiceConnection _productService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/Product/";

        public ProductServiceAccess()
        {
            _productService = new ServiceConnection(_serviceBaseUrl);
        }
        public async Task<List<Product>> GetProducts(int id = -1)
        {
            List<Product>? productFromService = null;

            if(_productService != null) {
                _productService.UseUrl = _productService.UseUrl;
                bool oneProductById = (id > 0);
                if (oneProductById)
                {
                    _productService.UseUrl += id;
                }
                try
                {
                    var serviceResponse = await _productService.CallServiceGet();

                    if(serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                    {

                        if( serviceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string responseData = await serviceResponse.Content.ReadAsStringAsync();
                            if (oneProductById)
                            {
                                Product? foundProduct = JsonConvert.DeserializeObject<Product>(responseData);
                                if (foundProduct != null) 
                                {
                                    productFromService = new List<Product> { foundProduct };
                                }
                            }
                            else
                            {
                                productFromService = JsonConvert.DeserializeObject<List<Product>>(responseData);
                            }  
                        } else if(serviceResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            productFromService = new List<Product>();
                        }
                    }
                }
                catch {
                    productFromService = null;
                }
            }
            return productFromService;
        }




        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = new List<Product>();

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
            Product product = new Product();

            HttpResponseMessage? response = await _productService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
                product = products.FirstOrDefault(p => p.ProductID == productId);
            }

            return product;
        }

        public async Task<int> AddProduct(Product productToAdd)
        {
            int insertedProductID = -1;

            _productService.UseUrl = _productService.BaseUrl;
            try
            {
                string productJson = JsonConvert.SerializeObject(productToAdd);
                var httpContent = new StringContent(JsonConvert.SerializeObject(productToAdd), Encoding.UTF8, "application/json");

                var serviceResponse = await _productService.CallServicePost(httpContent);

                if(serviceResponse is not null && serviceResponse.IsSuccessStatusCode)
                {
                    string idString = await serviceResponse.Content.ReadAsStringAsync();
                    bool idNumOk = int.TryParse(idString, out insertedProductID);
                    if (!idNumOk)
                    {
                        insertedProductID = -2;
                    }
                }
            } catch
            {
                insertedProductID = -3;
            }

            return insertedProductID;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            HttpResponseMessage? response = await _productService.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            _productService.UseUrl += $"/{productId}";

            HttpResponseMessage? response = await _productService.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
