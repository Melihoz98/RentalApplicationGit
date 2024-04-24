using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.ModelConversion;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class BusinessCustomerdataLogic : IBusinessCustomerdata
    {
        private readonly IBusinessCustomerAccess _businessCustomerAccess;

        public BusinessCustomerdataLogic(IBusinessCustomerAccess businessCustomerAccess)
        {
            _businessCustomerAccess = businessCustomerAccess;
        }

        public BusinessCustomerDto? GetById(int id)
        {
            BusinessCustomerDto? foundCustomerDto;
            try
            {
                BusinessCustomer? foundCustomer = _businessCustomerAccess.GetBusinessCustomerById(id);
                foundCustomerDto = BusinessCustomerDtoConvert.FromBusinessCustomer(foundCustomer);
            }
            catch
            {
                foundCustomerDto = null;
            }
            return foundCustomerDto;
        }

        public List<BusinessCustomerDto?>? GetAll()
        {
            List<BusinessCustomerDto?>? foundDtos;
            try
            {
                List<BusinessCustomer>? foundCustomers = _businessCustomerAccess.GetAllBusinessCustomers();
                foundDtos = BusinessCustomerDtoConvert.FromBusinessCustomerCollection(foundCustomers);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string errorMessage = ex.Message;
                // Handle exception
            }
            return foundDtos;
        }

        public int Add(BusinessCustomerDto businessCustomerDto)
        {
            int insertedId = 0;
            try
            {
                BusinessCustomer? dbCustomer = BusinessCustomerDtoConvert.ToBusinessCustomer(businessCustomerDto);
                if (dbCustomer != null)
                {
                    insertedId = _businessCustomerAccess.AddBusinessCustomer(dbCustomer);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                // Handle exception
            }
            return insertedId;
        }
    }
}
