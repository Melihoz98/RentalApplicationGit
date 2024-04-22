using RentalService.DTO;
using RentalService.Models;
namespace RentalService.Business
{
    public interface IProductCopyData
    {
        ProductCopyDto GetBySerialNumber(string serialNumber);

        List<ProductCopyDto?>? GetProductCopiesAll();
    }
}
