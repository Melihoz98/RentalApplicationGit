using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IPrivateCustomerAccess
    {
        Task<PrivateCustomer> GetPrivateCustomerById(string customerId);
        Task AddPrivateCustomer(PrivateCustomer customer);
        Task<bool> UpdatePrivateCustomer(PrivateCustomer customer);
        Task<bool> CustomerExists(string customerId);

    }
}
