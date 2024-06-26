﻿using Newtonsoft.Json;
using RentAppMVC.Models;
using System.Text;

namespace RentAppMVC.ServiceLayer
{
    public class BusinessCustomerAccess : IBusinessCustomerAccess
    {
        readonly IServiceConnection _businessCustomerService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/BusinessCustomer/";
        private readonly IPrivateCustomerAccess _privateCustomerAccess;

        public BusinessCustomerAccess()
        {
            _businessCustomerService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<BusinessCustomer> GetBusinessCustomerById(string customerId)
        {
            BusinessCustomer customer = new BusinessCustomer();

            HttpResponseMessage? response = await _businessCustomerService.GetById(customerId);
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<BusinessCustomer>(jsonString);
            }

            return customer;
        }

        public async Task AddBusinessCustomer(BusinessCustomer customer)
        {
            try
            {
                string customerJson = JsonConvert.SerializeObject(customer);
                var httpContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

                var serviceResponse = await _businessCustomerService.CallServicePost(httpContent);

                if (serviceResponse == null || !serviceResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to add business customer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding business customer: {ex.Message}");
                throw;
            }
        }



        public async Task<bool> CustomerExists(string customerId)
        {
            var businessCustomer = await GetBusinessCustomerById(customerId);
            if (businessCustomer != null && !string.IsNullOrEmpty(businessCustomer.CustomerID))
            {
                return true;
            }

            var privateCustomer = await _privateCustomerAccess.GetPrivateCustomerById(customerId);
            if (privateCustomer != null && !string.IsNullOrEmpty(privateCustomer.CustomerID))
            {
                return true;
            }

            return false;
        }

    }
}
