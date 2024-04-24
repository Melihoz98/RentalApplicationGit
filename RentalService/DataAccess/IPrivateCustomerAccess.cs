using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IPrivateCustomerAccess
    {

        PrivateCustomer GetPrivateCustomerById(int id);
        List<PrivateCustomer> GetAllPrivateCustomers();
        public int AddPrivateCustomer(PrivateCustomer privateCustomer);
    

    }
}
