using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface IProductCopyAccess
    {
        List<ProductCopy> GetProductCopyAll();
        ProductCopy GetProductCopyBySerialNumber(string serialNumber);
        List<ProductCopy> GetAllProductCopyByProductID(int productID);
        public List<ProductCopy> GetAllAvailableProductCopyByProductID(int productID, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime);
        void AddProductCopy(ProductCopy productCopy);
        void UpdateProductCopy(ProductCopy productCopy);
        void DeleteProductCopy(string serialNumber);
    }
}
