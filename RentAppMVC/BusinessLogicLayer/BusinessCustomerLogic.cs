using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;
using System;
using System.Collections.Generic;

namespace RentAppMVC.BusinessLogicLayer
{
    public class BusinessCustomerLogic
    {
        private readonly IBusinessCustomerAccess _businessCustomerAccess;
        private readonly IPrivateCustomerAccess _privateCustomerAccess;

        public BusinessCustomerLogic(IPrivateCustomerAccess privateCustomerAccess, IBusinessCustomerAccess businessCustomerAccess)
        {
            _privateCustomerAccess = privateCustomerAccess;
            _businessCustomerAccess = businessCustomerAccess;
        }


        public async Task<BusinessCustomer> GetBusinessCustomerById(string customerId)
        {
            return await _businessCustomerAccess.GetBusinessCustomerById(customerId);
        }

        public async Task AddBusinessCustomer(BusinessCustomer customer)
        {
            await _businessCustomerAccess.AddBusinessCustomer(customer);
        }

        public async Task<bool> CustomerExists(string customerId)
        {
            var privateCustomer = await _privateCustomerAccess.GetPrivateCustomerById(customerId);
            var businessCustomer = await _businessCustomerAccess.GetBusinessCustomerById(customerId);
            return privateCustomer != null || businessCustomer != null;
        }

    }
}
