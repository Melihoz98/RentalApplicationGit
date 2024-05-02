using RentalService.Models;

namespace RentalService.DataAccess 
{
    public interface IBusinessCustomerAccess
    {
        List<BusinessCustomer> GetAllBusinessCustomers();
        BusinessCustomer GetBusinessCustomerByCustomerID(string customerID);
        void CreateBusinessCustomer(BusinessCustomer customer);
    }
}
