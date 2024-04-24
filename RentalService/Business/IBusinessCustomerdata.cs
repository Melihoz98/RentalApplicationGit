using RentalService.DTO;

namespace RentalService.Business
{
    public interface IBusinessCustomerdata
    {


        BusinessCustomerDto? GetById(int id);
        List<BusinessCustomerDto?>? GetAll();

        int Add(BusinessCustomerDto businessCustomerDto);

    }
}
