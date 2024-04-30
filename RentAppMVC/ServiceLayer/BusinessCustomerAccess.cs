using Newtonsoft.Json;
using RentAppMVC.Models;
using System.Text;

namespace RentAppMVC.ServiceLayer
{
    public class BusinessCustomerAccess
    {
        readonly IServiceConnection _businessCustomerService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/BusinessCustomer/";

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

        public async Task<int> AddBusinessCustomer(BusinessCustomer customer)
        {
            int insertedCustomerId = -1;

            try
            {
                string customerJson = JsonConvert.SerializeObject(customer);
                var httpContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

                var serviceResponse = await _businessCustomerService.CallServicePost(httpContent);

                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    string idString = await serviceResponse.Content.ReadAsStringAsync();
                    bool idNumOk = int.TryParse(idString, out insertedCustomerId);
                    if (!idNumOk)
                    {
                        insertedCustomerId = -2;
                    }
                }
            }
            catch
            {
                insertedCustomerId = -3;
            }

            return insertedCustomerId;
        }

        public async Task<bool> UpdateBusinessCustomer(BusinessCustomer customer)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

            HttpResponseMessage? response = await _businessCustomerService.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
