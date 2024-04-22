using RentalService.Models;
using System;
namespace RentalService.DataAccess
{
    public interface IProductCopyAccess
    {
        ProductCopy GetBySerialNumber(string serialNumber);

        List<ProductCopy> GetProductCopiesAll();

    }
}
