using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class PrivateCustomerLogic
    {
        private readonly IPrivateCustomerAccess _privateCustomerAccess;
        private readonly IBusinessCustomerAccess _businessCustomerAccess;
        public PrivateCustomerLogic(IPrivateCustomerAccess privateCustomerAccess, IBusinessCustomerAccess businessCustomerAccess)
        {
            _privateCustomerAccess = privateCustomerAccess;
            _businessCustomerAccess = businessCustomerAccess;
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

        public async Task<bool> CustomerExists(string customerId)
        {
            var privateCustomer = await _privateCustomerAccess.GetPrivateCustomerById(customerId);
            var businessCustomer = await _businessCustomerAccess.GetBusinessCustomerById(customerId);
            return privateCustomer != null || businessCustomer != null;
        }
    }
}
