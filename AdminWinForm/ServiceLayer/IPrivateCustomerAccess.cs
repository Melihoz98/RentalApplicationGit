using AdminWinForm.Models;

namespace AdminWinForm.ServiceLayer
{
    public interface IPrivateCustomerAccess
    {
        Task<List<PrivateCustomer>> GetPrivateCustomers();
        Task<PrivateCustomer> GetPrivateCustomerById(string customerId);
    }
}
