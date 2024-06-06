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

        public async Task<bool> CustomerExists(string customerId)
        {
            var privateCustomer = await GetPrivateCustomerById(customerId);
            if (privateCustomer != null && !string.IsNullOrEmpty(privateCustomer.CustomerID))
            {
                return true;
            }

            var businessCustomer = await _businessCustomerAccess.GetBusinessCustomerById(customerId);
            if (businessCustomer != null && !string.IsNullOrEmpty(businessCustomer.CustomerID))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsPrivateCustomer(string customerId)
        {
            var privateCustomer = await GetPrivateCustomerById(customerId);
            return privateCustomer != null && !string.IsNullOrEmpty(privateCustomer.CustomerID);
        }

    }
}
