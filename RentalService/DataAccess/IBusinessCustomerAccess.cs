using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IBusinessCustomerAccess
    {

        BusinessCustomer GetBusinessCustomerById(int id);
        List<BusinessCustomer> GetAllBusinessCustomers();
        int AddBusinessCustomer(BusinessCustomer businessCustomer);
       
        

    }
}
