using Newtonsoft.Json;
using AdminWinForm.Models;
using System.Text;
using AdminWinForm.BusinesslogicLayer;
namespace AdminWinForm.ServiceLayer
{
    public class PrivateCustomerServiceAccess : IPrivateCustomerAccess
    {
        readonly IServiceConnection _privateCustomerService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/PrivateCustomer/";

        public PrivateCustomerServiceAccess()
        {
            _privateCustomerService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<PrivateCustomer>> GetPrivateCustomers()
        {
            List<PrivateCustomer> customers = new List<PrivateCustomer>();

            HttpResponseMessage? response = await _privateCustomerService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<PrivateCustomer>>(jsonString);
            }

            return customers;
        }

        public async Task<PrivateCustomer> GetPrivateCustomerById(string customerId)
        {
            PrivateCustomer customer = new PrivateCustomer();

            HttpResponseMessage? response = await _privateCustomerService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<PrivateCustomer>(jsonString);
            }

            return customer;
        }
    
    }
}
