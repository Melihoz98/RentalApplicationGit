using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IPrivateCustomerAccess
    {

        PrivateCustomer GetPrivateCustomerById(string id);
        List<PrivateCustomer> GetAllPrivateCustomers();
        void AddPrivateCustomer(PrivateCustomer privateCustomer);
        void UpdatePrivateCustomer(PrivateCustomer customer);
        void DeletePrivateCustomer(string customerID);

    }
}
