using Newtonsoft.Json;
using RentAppMVC.Models;
using System.Text;

namespace RentAppMVC.ServiceLayer
{
    public class PrivateCustomerAccess : IPrivateCustomerAccess
    {
        readonly IServiceConnection _privateCustomerService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/PrivateCustomer/";

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

        public async Task<int> AddPrivateCustomer(PrivateCustomer customer)
        {
            int insertedCustomerId = -1;

            try
            {
                string customerJson = JsonConvert.SerializeObject(customer);
                var httpContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

                var serviceResponse = await _privateCustomerService.CallServicePost(httpContent);

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

        public async Task<bool> UpdatePrivateCustomer(PrivateCustomer customer)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

            HttpResponseMessage? response = await _privateCustomerService.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }

    
    }
}
