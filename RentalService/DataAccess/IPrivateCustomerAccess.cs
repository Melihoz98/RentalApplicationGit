using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IPrivateCustomerAccess
    {

        PrivateCustomer GetPrivateCustomerById(string id);
        List<PrivateCustomer> GetAllPrivateCustomers();
        void AddPrivateCustomer(PrivateCustomer privateCustomer);

        void DeletePrivateCustomer(string customerID);
    }
}
