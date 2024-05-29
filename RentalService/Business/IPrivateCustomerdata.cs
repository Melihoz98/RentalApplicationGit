using RentalService.DTO;

namespace RentalService.Business
{
    public interface IPrivateCustomerData
    {
        List<PrivateCustomerDto?>? GetAllPrivateCustomers();
        PrivateCustomerDto? GetPrivateCustomerById(string id);
        void CreatePrivateCustomer(PrivateCustomerDto privateCustomerDto);

        void DeletePrivateCustomer(string id);
    }
}