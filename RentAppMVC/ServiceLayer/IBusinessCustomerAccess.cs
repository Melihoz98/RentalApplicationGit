using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IBusinessCustomerAccess
    {
        Task<BusinessCustomer> GetBusinessCustomerById(string customerId);
        Task<int> AddBusinessCustomer(BusinessCustomer customer);
        Task<bool> UpdateBusinessCustomer(BusinessCustomer customer);
    }
}
