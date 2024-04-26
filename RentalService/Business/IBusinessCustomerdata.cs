using RentalService.DTO;

namespace RentalService.Business
{
    public interface IBusinessCustomerData
    {
        List<BusinessCustomerDto> GetAllBusinessCustomers();
        BusinessCustomerDto GetBusinessCustomerByCustomerID(string customerID);
        void CreateBusinessCustomer(BusinessCustomerDto customerToAdd);
    }
}
