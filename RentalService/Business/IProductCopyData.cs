using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface IProductCopyData
    {
        ProductCopyDto? GetBySerialNumber(string serialNumber);
        List<ProductCopyDto?>? GetAllProductCopies();
        List<ProductCopyDto?>? GetAllProductCopiesByProductID(int productID);
        public List<ProductCopyDto> GetAllAvailableProductCopyByProductID(int productID, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime);
        void CreateProductCopy(ProductCopyDto productCopyToAdd);
       
        void DeleteProductCopy(string serialNumber);
    }
}
