using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IPrivateCustomerAccess
    {

        PrivateCustomer GetPrivateCustomerById(string customerID);
        List<PrivateCustomer> GetAllPrivateCustomers();
        void CeatePrivateCustomer(PrivateCustomer customer);
        void UpdatePrivateCustomer(PrivateCustomer customer);
        void DeletePrivateCustomer(string customerID);

    }
}
