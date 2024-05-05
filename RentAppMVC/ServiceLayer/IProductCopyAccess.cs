using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IProductCopyAccess
    {
        Task<ProductCopy?> GetBySerialNumber(string serialNumber);
        Task<List<ProductCopy>?> GetAllProductCopies();
    }
}
