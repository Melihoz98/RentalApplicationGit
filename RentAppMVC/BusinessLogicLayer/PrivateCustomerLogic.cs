using System.Collections.Generic;
using System.Threading.Tasks;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class PrivateCustomerLogic
    {
        private readonly IPrivateCustomerAccess _privateCustomerAccess;

        public PrivateCustomerLogic()
        {
            _privateCustomerAccess = new PrivateCustomerAccess();
        }

        public async Task<PrivateCustomer> GetPrivateCustomerById(string customerId)
        {
            return await _privateCustomerAccess.GetPrivateCustomerById(customerId);
        }

        public async Task AddPrivateCustomer(PrivateCustomer customer)
        {
            await _privateCustomerAccess.AddPrivateCustomer(customer);
        }

        public async Task UpdatePrivateCustomer(PrivateCustomer customer)
        {
            await _privateCustomerAccess.UpdatePrivateCustomer(customer);
        }

    }
}
