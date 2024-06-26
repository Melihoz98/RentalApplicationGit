﻿using Newtonsoft.Json;
using RentAppMVC.Models;
using System.Text;

namespace RentAppMVC.ServiceLayer
{
    public class PrivateCustomerAccess : IPrivateCustomerAccess
    {
        readonly IServiceConnection _privateCustomerService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/PrivateCustomer/";
        private readonly IBusinessCustomerAccess _businessCustomerAccess;

        public PrivateCustomerAccess()
        {
            _privateCustomerService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<PrivateCustomer> GetPrivateCustomerById(string customerId)
        {
            PrivateCustomer customer = new PrivateCustomer();

            HttpResponseMessage? response = await _privateCustomerService.GetById(customerId);
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<PrivateCustomer>(jsonString);
            }

            return customer;
        }

        public async Task AddPrivateCustomer(PrivateCustomer customer)
        {
            try
            {
                string customerJson = JsonConvert.SerializeObject(customer);
                var httpContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

                var serviceResponse = await _privateCustomerService.CallServicePost(httpContent);

                if (serviceResponse == null || !serviceResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to add private customer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding private customer: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdatePrivateCustomer(PrivateCustomer customer)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

            HttpResponseMessage? response = await _privateCustomerService.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
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


    }
}
