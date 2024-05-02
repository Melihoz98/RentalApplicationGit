using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.ModelConversion;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class PrivateCustomerDataLogic : IPrivateCustomerData
    {
        private readonly IPrivateCustomerAccess _privateCustomerAccess;

        public PrivateCustomerDataLogic(IPrivateCustomerAccess privateCustomerAccess)
        {
            _privateCustomerAccess = privateCustomerAccess;
        }

        public PrivateCustomerDto? GetPrivateCustomerById(string id)
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

        public List<PrivateCustomerDto?>? GetAllPrivateCustomers()
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

        public void createPrivateCustomer(PrivateCustomerDto customerToAdd)
        {
            try
            {
                PrivateCustomer customer = PrivateCustomerDtoConvert.ToPrivateCustomer(customerToAdd);
                _privateCustomerAccess.CeatePrivateCustomer(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating business customer: {ex.Message}");
                throw;
            }

        }

        public void UpdatePrivateCustomer(PrivateCustomerDto privateCustomerDto)
        {
            try
            {
                PrivateCustomer dbCustomer = PrivateCustomerDtoConvert.ToPrivateCustomer(privateCustomerDto);
                _privateCustomerAccess.UpdatePrivateCustomer(dbCustomer);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                // Handle exception
            }
        }

        public void DeletePrivateCustomer(string id)
        {
            try
            {
                _privateCustomerAccess.DeletePrivateCustomer(id);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                // Handle exception
            }
        }

    }
}
