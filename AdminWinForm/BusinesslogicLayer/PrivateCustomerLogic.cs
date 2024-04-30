using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;

namespace AdminWinForm.BusinesslogicLayer
{
    public class PrivateCustomerLogic
    {
        readonly IPrivateCustomerAccess _privateCustomerAccess;

        public PrivateCustomerLogic()
        {
            _privateCustomerAccess = new PrivateCustomerServiceAccess();
        }

        public async Task<List<PrivateCustomer>?> GetAllPrivateCustomers()
        {
            List<PrivateCustomer>? foundCustomers = null;
            if (_privateCustomerAccess != null)
            {
                foundCustomers = await _privateCustomerAccess.GetPrivateCustomers();
            }
            return foundCustomers;
        }

        public async Task<PrivateCustomer> GetPrivateCustomerById(string customerId)
        {
            PrivateCustomer customer = new PrivateCustomer();
            if (_privateCustomerAccess != null)
            {
                customer = await _privateCustomerAccess.GetPrivateCustomerById(customerId);
            }
            return customer;
        }
    }
}
