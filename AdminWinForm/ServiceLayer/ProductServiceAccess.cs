using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdminWinForm.Models;
using System.Linq.Expressions;
using System.Net;

namespace AdminWinForm.ServiceLayer
{
    public class ProductServiceAccess : IProductAccess
    {

        readonly IServiceConnection _productService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/Product/";
        static readonly string authenType = "Bearer";

        public HttpStatusCode CurrentHttpStatusCode { get; set; }
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

        public async Task<int> AddProduct(string tokenToUse, Product productToAdd)
        {
            int insertedProductID = -1;
            _productService.UseUrl = _productService.BaseUrl;

            // Must add Bearer token to request header
            string bearerTokenValue = authenType + " " + tokenToUse;
            _productService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization"); // To avoid more Authorization headers
            _productService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

            try
            {
                string productJson = JsonConvert.SerializeObject(productToAdd);
                var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");
                var serviceResponse = await _productService.CallServicePost(httpContent);

                CurrentHttpStatusCode = serviceResponse != null ? serviceResponse.StatusCode : HttpStatusCode.BadRequest; // Used by logic class

                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    string idString = await serviceResponse.Content.ReadAsStringAsync();
                    bool idNumOk = int.TryParse(idString, out insertedProductID);
                    if (!idNumOk)
                    {
                        insertedProductID = -4; // Adjusted to align with other method responses
                    }
                }
                else
                {
                    insertedProductID = -2; // Adjusted to align with other method responses
                }
            }
            catch
            {
                insertedProductID = -3;
            }
            return insertedProductID;
        }


        public async Task<bool> DeleteProduct(int productID)
        {
            _productService.UseUrl += $"{productID}";

            HttpResponseMessage? response = await _productService.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
