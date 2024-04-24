using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IBusinessCustomerAccess
    {

        BusinessCustomer GetBusinessCustomerById(int id);
        List<BusinessCustomer> GetBusinessCustomerAll();
        int AddBusinessCustomer(BusinessCustomer businessCustomer);
        void UpdateBusinessCustomer(BusinessCustomer businessCustomer);
        void DeleteBusinessCustomer(int id);

    }
}
