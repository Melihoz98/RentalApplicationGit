using RentalService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AdminWinForm.ServiceLayer
{
    public class ProductCopyServiceAccess : IProductCopyAccess
    {
        private readonly IServiceConnection _productCopyService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/ProductCopy/";

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

        public async Task<int> AddProductCopy(ProductCopy productCopy)
        {
            int insertedProductCopyID = -1;

            _productCopyService.UseUrl = _productCopyService.BaseUrl;
            try
            {
                string productCopyJson = JsonConvert.SerializeObject(productCopy);
                var httpContent = new StringContent(JsonConvert.SerializeObject(productCopy), Encoding.UTF8, "application/json");

                var serviceResponse = await _productCopyService.CallServicePost(httpContent);

                if (serviceResponse is not null && serviceResponse.IsSuccessStatusCode)
                {
                    string idString = await serviceResponse.Content.ReadAsStringAsync();
                    bool idNumOk = int.TryParse(idString, out insertedProductCopyID);
                    if (!idNumOk)
                    {
                        insertedProductCopyID = -2;
                    }
                }
            }
            catch
            {
                insertedProductCopyID = -3;
            }

            return insertedProductCopyID;
        }

        public async Task<bool> UpdateProductCopy(ProductCopy productCopy)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(productCopy), Encoding.UTF8, "application/json");

            HttpResponseMessage? response = await _productCopyService.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
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
