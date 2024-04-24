using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.ModelConversion;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class PrivateCustomerdataLogic : IPrivateCustomerdata
    {
        private readonly IPrivateCustomerAccess _privateCustomerAccess;

        public PrivateCustomerdataLogic(IPrivateCustomerAccess privateCustomerAccess)
        {
            _privateCustomerAccess = privateCustomerAccess;
        }

        public PrivateCustomerDto? GetById(int id)
        {
            PrivateCustomerDto? foundCustomerDto;
            try
            {
                PrivateCustomer? foundCustomer = _privateCustomerAccess.GetPrivateCustomerById(id);
                foundCustomerDto = PrivateCustomerDtoConvert.FromPrivateCustomer(foundCustomer);
            }
            catch
            {
                foundCustomerDto = null;
            }
            return foundCustomerDto;
        }

        public List<PrivateCustomerDto?>? GetAll()
        {
            List<PrivateCustomerDto?>? foundDtos;
            try
            {
                List<PrivateCustomer>? foundCustomers = _privateCustomerAccess.GetAllPrivateCustomers();
                foundDtos = PrivateCustomerDtoConvert.FromPrivateCustomerCollection(foundCustomers);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string errorMessage = ex.Message;
                // Handle exception
            }
            return foundDtos;
        }

        public int Add(PrivateCustomerDto privateCustomerDto)
        {
            int insertedId = 0;
            try
            {
                PrivateCustomer? dbCustomer = PrivateCustomerDtoConvert.ToPrivateCustomer(privateCustomerDto);
                if (dbCustomer != null)
                {
                    insertedId = _privateCustomerAccess.AddPrivateCustomer(dbCustomer);
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
