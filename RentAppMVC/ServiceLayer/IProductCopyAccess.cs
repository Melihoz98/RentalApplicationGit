using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IProductCopyAccess
    {
        Task<ProductCopy?> GetBySerialNumber(string serialNumber);
        Task<List<ProductCopy>?> GetAllProductCopies();

        Task<List<ProductCopy>?> GetAllProductCopiesById(int productID);
    }

}
