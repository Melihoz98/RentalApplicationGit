using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IPrivateCustomerAccess
    {

        PrivateCustomer GetPrivateCustomerById(int id);
        List<PrivateCustomer> GetPrivateCustomerAll();
        int AddPrivateCustomer(PrivateCustomer privateCustomer);
        void UpdatePrivateCustomer(PrivateCustomer privateCustomer);
        void DeletePrivateCustomert(int id);

    }
}
