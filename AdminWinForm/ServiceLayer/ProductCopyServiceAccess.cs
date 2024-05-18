using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace AdminWinForm.ServiceLayer
{
    public class ProductCopyServiceAccess : IProductCopyAccess
    {
        private readonly IServiceConnection _productCopyService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/ProductCopy/";
        static readonly string authenType = "Bearer";

        public HttpStatusCode CurrentHttpStatusCode { get; set; }

        public ProductCopyServiceAccess()
        {
            _productCopyService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<ProductCopy>> GetProductCopies()
        {
            List<ProductCopy> productCopies = new List<ProductCopy>();

            HttpResponseMessage? response = await _productCopyService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                productCopies = JsonConvert.DeserializeObject<List<ProductCopy>>(jsonString);
            }

            return productCopies;
        }

        public async Task<ProductCopy> GetProductCopyById(int productId)
        {
            ProductCopy productCopy = new ProductCopy();

            HttpResponseMessage? response = await _productCopyService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<ProductCopy> productCopies = JsonConvert.DeserializeObject<List<ProductCopy>>(jsonString);
                productCopy = productCopies.FirstOrDefault(pc => pc.ProductID == productId);
            }

            return productCopy;
        }

        public async Task<int> AddProductCopy(string tokenToUse, ProductCopy productCopy)
        {
            int insertedProductCopyID = -1;

            _productCopyService.UseUrl = _productCopyService.BaseUrl;

            // Must add Bearer token to request header
            string bearerTokenValue = authenType + " " + tokenToUse;
            _productCopyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization"); // To avoid more Authorization headers
            _productCopyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

            try
            {
                string productCopyJson = JsonConvert.SerializeObject(productCopy);
                var httpContent = new StringContent(productCopyJson, Encoding.UTF8, "application/json");

                var serviceResponse = await _productCopyService.CallServicePost(httpContent);

                CurrentHttpStatusCode = serviceResponse != null ? serviceResponse.StatusCode : System.Net.HttpStatusCode.BadRequest; // Used by logic class

                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    string idString = await serviceResponse.Content.ReadAsStringAsync();
                    bool idNumOk = int.TryParse(idString, out insertedProductCopyID);
                    if (!idNumOk)
                    {
                        insertedProductCopyID = -2; // Adjusted to align with other method responses
                    }
                }
                else
                {
                    insertedProductCopyID = -3; // Adjusted to align with other method responses
                }
            }
            catch
            {
                insertedProductCopyID = -4;
            }

            return insertedProductCopyID;
        }

        public async Task<bool> DeleteProductCopy(string serialNumber)
        {
            _productCopyService.UseUrl += $"{serialNumber}";

            HttpResponseMessage? response = await _productCopyService.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }

        public Task<ProductCopy> GetProductCopyBySerialNumberAndProductId(string serialNumber, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
