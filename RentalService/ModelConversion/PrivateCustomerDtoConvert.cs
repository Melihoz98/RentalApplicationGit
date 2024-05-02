using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class PrivateCustomerDtoConvert
    {
        public static List<PrivateCustomerDto> FromPrivateCustomerCollection(List<PrivateCustomer> customers)
        {
            List<PrivateCustomerDto> customerDtos = new List<PrivateCustomerDto>();
            foreach (var customer in customers)
            {
                customerDtos.Add(FromPrivateCustomer(customer));
             }

            return customerDtos;
        }

        public static PrivateCustomerDto? FromPrivateCustomer(PrivateCustomer customer)
        {
            return new PrivateCustomerDto
            {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber
            };
            

        }

        public static PrivateCustomer ToPrivateCustomer(PrivateCustomerDto dto)
        {
            return new PrivateCustomer
            {
                CustomerID = dto.CustomerID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };
        }
    }
}
