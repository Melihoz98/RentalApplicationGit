using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class BusinessCustomerDtoConvert
    {
       public static List<BusinessCustomerDto> FromBusinessCustomerCollection(List<BusinessCustomer> customers)
        {
            List<BusinessCustomerDto> customerDtos = new List<BusinessCustomerDto>();
            foreach (var customer in customers)
            {
                customerDtos.Add(FromBusinessCustomer(customer));
            }
            return customerDtos;
        }

          public static BusinessCustomerDto FromBusinessCustomer(BusinessCustomer customer)
          {
            return new BusinessCustomerDto
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName,
                CVR = customer.CVR,
                PhoneNumber = customer.PhoneNumber
            };
          }

        public static BusinessCustomer ToBusinessCustomer(BusinessCustomerDto customerDto)
        {
            return new BusinessCustomer
            {
                CustomerID = customerDto.CustomerID,
                CompanyName = customerDto.CompanyName,
                CVR = customerDto.CVR,
                PhoneNumber = customerDto.PhoneNumber
            };
        }
    }
}
