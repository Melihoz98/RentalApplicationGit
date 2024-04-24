using RentalService.Models;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class BusinessCustomerDtoConvert
    {
        public static List<BusinessCustomerDto?>? FromBusinessCustomerCollection(List<BusinessCustomer> inCustomers)
        {
            List<BusinessCustomerDto?>? customerDtos = null;
            if (inCustomers != null)
            {
                customerDtos = new List<BusinessCustomerDto?>();
                foreach (BusinessCustomer customer in inCustomers)
                {
                    BusinessCustomerDto? dto = FromBusinessCustomer(customer);
                    customerDtos.Add(dto);
                }
            }
            return customerDtos;
        }

        public static BusinessCustomerDto? FromBusinessCustomer(BusinessCustomer customer)
        {
            BusinessCustomerDto? dto = null;
            if (customer != null)
            {
                dto = new BusinessCustomerDto
                {
                    BusinessCustomerID = customer.BusinessCustomerID,
                    CompanyName = customer.CompanyName,
                    CVR = customer.CVR,
                    UserID = customer.UserID,
                    PhoneNumber = customer.PhoneNumber
                };
            }
            return dto;
        }

        public static BusinessCustomer ToBusinessCustomer(BusinessCustomerDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new BusinessCustomer
            {
                BusinessCustomerID = dto.BusinessCustomerID,
                CompanyName = dto.CompanyName,
                CVR = dto.CVR,
                UserID = dto.UserID,
                PhoneNumber = dto.PhoneNumber
            };
        }
    }
}
