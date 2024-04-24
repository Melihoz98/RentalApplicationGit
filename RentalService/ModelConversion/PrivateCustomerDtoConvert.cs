using RentalService.Models;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class PrivateCustomerDtoConvert
    {
        public static List<PrivateCustomerDto?>? FromPrivateCustomerCollection(List<PrivateCustomer> inCustomers)
        {
            List<PrivateCustomerDto?>? customerDtos = null;
            if (inCustomers != null)
            {
                customerDtos = new List<PrivateCustomerDto?>();
                foreach (PrivateCustomer customer in inCustomers)
                {
                    PrivateCustomerDto? dto = FromPrivateCustomer(customer);
                    customerDtos.Add(dto);
                }
            }
            return customerDtos;
        }

        public static PrivateCustomerDto? FromPrivateCustomer(PrivateCustomer customer)
        {
            PrivateCustomerDto? dto = null;
            if (customer != null)
            {
                dto = new PrivateCustomerDto
                {
                    PrivateCustomerID = customer.PrivateCustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    UserID = customer.UserID,
                    PhoneNumber = customer.PhoneNumber
                };
            }
            return dto;
        }

        public static PrivateCustomer ToPrivateCustomer(PrivateCustomerDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new PrivateCustomer
            {
                PrivateCustomerID = dto.PrivateCustomerID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserID = dto.UserID,
                PhoneNumber = dto.PhoneNumber
            };
        }
    }
}
