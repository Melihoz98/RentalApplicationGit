using RentalService.DTO;

namespace RentalService.Business
{
    public interface IPrivateCustomerdata
    {

        PrivateCustomerDto? GetById(int id);

        List<PrivateCustomerDto?>? GetAll();
        int Add(PrivateCustomerDto privateCustomerDto);

    }
}
