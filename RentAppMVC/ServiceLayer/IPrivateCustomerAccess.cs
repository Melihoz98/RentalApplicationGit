using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IPrivateCustomerAccess
    {
        Task<PrivateCustomer> GetPrivateCustomerById(string customerId);
        Task<int> AddPrivateCustomer(PrivateCustomer customer);
        Task<bool> UpdatePrivateCustomer(PrivateCustomer customer);

    }
}
