using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public class ProductCopyAccess : IProductCopyAccess
    {
        private readonly IServiceConnection _productCopyService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/ProductCopy/";

        public ProductCopyAccess()
        {
            _productCopyService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<ProductCopy?> GetBySerialNumber(string serialNumber)
        {
            ProductCopy? productCopy = null;

            HttpResponseMessage? response = await _productCopyService.GetById(serialNumber);
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                productCopy = JsonConvert.DeserializeObject<ProductCopy>(jsonString);
            }

            return productCopy;
        }

        public async Task<List<ProductCopy>?> GetAllProductCopies()
        {
            List<ProductCopy>? productCopies = null;

            HttpResponseMessage? response = await _productCopyService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                productCopies = JsonConvert.DeserializeObject<List<ProductCopy>>(jsonString);
            }

            return productCopies;
        }
        public async Task<List<ProductCopy>> GetAllProductCopiesById(int productID)
        {
            List<ProductCopy> productCopies = new List<ProductCopy>();

            HttpResponseMessage? response = await _productCopyService.Get($"{_serviceBaseUrl}product/{productID}"); // Target specific endpoint

            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                productCopies = JsonConvert.DeserializeObject<List<ProductCopy>>(jsonString);
            }

            return productCopies;
        }





    }
}
