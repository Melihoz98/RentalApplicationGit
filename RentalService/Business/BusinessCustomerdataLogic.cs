using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using RentalService.ModelConversion;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class BusinessCustomerDataLogic : IBusinessCustomerData
    {
        private readonly IBusinessCustomerAccess _businessCustomerAccess;

        public BusinessCustomerDataLogic(IBusinessCustomerAccess businessCustomerAccess)
        {
            _businessCustomerAccess = businessCustomerAccess;
        }

        public List<BusinessCustomerDto> GetAllBusinessCustomers()
        {
            try
            {
                List<BusinessCustomer> customers = _businessCustomerAccess.GetAllBusinessCustomers();
                return BusinessCustomerDtoConvert.FromBusinessCustomerCollection(customers);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting all business customers: {ex.Message}");
                throw; 
            }
        }

        public BusinessCustomerDto GetBusinessCustomerByCustomerID(string customerID)
        {
            try
            {
                BusinessCustomer customer = _businessCustomerAccess.GetBusinessCustomerByCustomerID(customerID);
                if (customer == null)
                {
                    return null; 
                }
                return BusinessCustomerDtoConvert.FromBusinessCustomer(customer);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting business customer by ID: {ex.Message}");
                throw; 
            }
        }

        public void CreateBusinessCustomer(BusinessCustomerDto customerToAdd)
        {
            try
            {
                BusinessCustomer customer = BusinessCustomerDtoConvert.ToBusinessCustomer(customerToAdd);
                _businessCustomerAccess.CreateBusinessCustomer(customer);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error creating business customer: {ex.Message}");
                throw; 
            }
        }
        public void RemoveBusinessCustomer(string customerID)
        {
            try
            {
                _businessCustomerAccess.RemoveBusinessCustomer(customerID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing business customer: {ex.Message}");
                throw;
            }
        }
    }
}
