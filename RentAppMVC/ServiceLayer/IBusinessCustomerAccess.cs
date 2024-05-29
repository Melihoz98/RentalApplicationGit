using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IBusinessCustomerAccess
    {
        Task<BusinessCustomer> GetBusinessCustomerById(string customerId);
        Task AddBusinessCustomer(BusinessCustomer customer);
        Task<bool> CustomerExists(string customerId);
    }
}
