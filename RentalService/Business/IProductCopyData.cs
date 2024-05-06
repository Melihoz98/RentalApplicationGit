using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface IProductCopyData
    {
        ProductCopyDto? GetBySerialNumber(string serialNumber);
        List<ProductCopyDto?>? GetAllProductCopies();
        List<ProductCopyDto?>? GetAllProductCopiesByID(int productID);
        void CreateProductCopy(ProductCopyDto productCopyToAdd);
        void UpdateProductCopy(ProductCopyDto productCopyToUpdate); 
        void DeleteProductCopy(string serialNumber);
    }
}
