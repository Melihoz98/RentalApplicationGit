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

            HttpResponseMessage? response = await _productCopyService.Get($"{_serviceBaseUrl}product/{productID}"); 

            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                productCopies = JsonConvert.DeserializeObject<List<ProductCopy>>(jsonString);
            }

            return productCopies;
        }

        public async Task<List<ProductCopy>?> GetAllAvailableProductCopyByProductID(int productID, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            List<ProductCopy>? availableProductCopies = new List<ProductCopy>();

            HttpResponseMessage? response = await _productCopyService.Get($"{_serviceBaseUrl}available/product/{productID}?startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&startTime={startTime.ToString()}&endTime={endTime.ToString()}");
            
            if (response != null && response.IsSuccessStatusCode)
            {

                string jsonString = await response.Content.ReadAsStringAsync();
                availableProductCopies = JsonConvert.DeserializeObject<List<ProductCopy>>(jsonString);
            }

            return availableProductCopies;
        }

    }
}
