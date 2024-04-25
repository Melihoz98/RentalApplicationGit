using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface IProductCopyAccess
    {
        List<ProductCopy> GetProductCopyAll();
        ProductCopy GetProductCopyBySerialNumber(string serialNumber);
        void AddProductCopy(ProductCopy productCopy);
        void UpdateProductCopy(ProductCopy productCopy);
        void DeleteProductCopy(string serialNumber);
    }
}
