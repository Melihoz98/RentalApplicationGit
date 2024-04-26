using RentalService.Models;

namespace RentalService.DataAccess // Assuming this namespace holds your interfaces
{
    public interface IBusinessCustomerAccess
    {
        List<BusinessCustomer> GetAllBusinessCustomers();
        BusinessCustomer GetBusinessCustomerByCustomerID(string customerID);
        void CreateBusinessCustomer(BusinessCustomer customer);
    }
}
