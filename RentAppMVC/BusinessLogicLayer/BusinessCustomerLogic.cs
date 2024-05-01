using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;
using System;
using System.Collections.Generic;

namespace RentAppMVC.BusinessLogicLayer
{
    public class BusinessCustomerLogic
    {
        private readonly IBusinessCustomerAccess _businessCustomerAccess;

        public BusinessCustomerLogic()
        {
            _businessCustomerAccess = new BusinessCustomerAccess();
        }

        public async Task<BusinessCustomer> GetBusinessCustomerById(string customerId)
        {
            return await _businessCustomerAccess.GetBusinessCustomerById(customerId);
        }

        public async Task AddBusinessCustomer(BusinessCustomer customer)
        {
            await _businessCustomerAccess.AddBusinessCustomer(customer);
        }

        public async Task UpdateBusinessCustomer(BusinessCustomer customer)
        {
            await _businessCustomerAccess.UpdateBusinessCustomer(customer);
        }

    }
}
