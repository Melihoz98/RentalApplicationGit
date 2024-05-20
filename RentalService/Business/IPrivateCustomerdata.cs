using RentalService.DTO;

namespace RentalService.Business
{
    public interface IPrivateCustomerData
    {
        List<PrivateCustomerDto?>? GetAllPrivateCustomers();
        PrivateCustomerDto? GetPrivateCustomerById(string id);
        int createPrivateCustomer(PrivateCustomerDto privateCustomerDto);

        void DeletePrivateCustomer(string id);
    }
}