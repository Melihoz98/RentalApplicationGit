using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class ProductCopyLogic
    {
        private readonly IProductCopyAccess _productCopyAccess;

        public ProductCopyLogic()
        {
            _productCopyAccess = new ProductCopyAccess();
        }

        public async Task<ProductCopy?> GetProductCopyBySerialNumber(string serialNumber)
        {
            return await _productCopyAccess.GetBySerialNumber(serialNumber);
        }

        public async Task<List<ProductCopy>?> GetAllProductCopies()
        {
            return await _productCopyAccess.GetAllProductCopies();
        }


        public async Task<List<ProductCopy>?> GetAllProductCopyByID(int productID)  {
        
          
            return await _productCopyAccess.GetAllProductCopiesById(productID);
        }

        public async Task<List<ProductCopy>> GetAllAvailableProductCopyByProductID(int productID, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            return await _productCopyAccess.GetAllAvailableProductCopyByProductID(productID, startDate, endDate, startTime, endTime);
        }

    }
}