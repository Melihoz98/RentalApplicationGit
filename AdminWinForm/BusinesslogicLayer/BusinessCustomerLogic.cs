using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;

namespace AdminWinForm.BusinesslogicLayer
{
    public class BusinessCustomerLogic
    {
        readonly IBusinessCustomerAccess _businessCustomerAccess;

        public BusinessCustomerLogic()
        {
            _businessCustomerAccess = new BusinessCustomerServiceAccess();
        }

        public async Task<List<BusinessCustomer>?> GetAllBusinessCustomers()
        {
            List<BusinessCustomer>? foundCustomers = null;
            if (_businessCustomerAccess != null)
            {
                foundCustomers = await _businessCustomerAccess.GetBusinessCustomers();
            }
            return foundCustomers;
        }

        public async Task<BusinessCustomer> GetBusinessCustomerById(string customerId)
        {
            BusinessCustomer customer = new BusinessCustomer();
            if (_businessCustomerAccess != null)
            {
                customer = await _businessCustomerAccess.GetBusinessCustomerById(customerId);
            }
            return customer;
        }
    }
}
