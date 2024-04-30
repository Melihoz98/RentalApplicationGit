using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdminWinForm.Models;

namespace AdminWinForm.ServiceLayer
{
    public class BusinessCustomerServiceAccess : IBusinessCustomerAccess
    {
        readonly IServiceConnection _businessCustomerService;
        readonly string _serviceBaseUrl = "https://localhost:7023/api/BusinessCustomer/";

        public BusinessCustomerServiceAccess()
        {
            _businessCustomerService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<BusinessCustomer>> GetBusinessCustomers()
        {
            List<BusinessCustomer> customers = new List<BusinessCustomer>();

            HttpResponseMessage? response = await _businessCustomerService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<BusinessCustomer>>(jsonString);
            }

            return customers;
        }

        public async Task<BusinessCustomer> GetBusinessCustomerById(string customerId)
        {
            BusinessCustomer customer = new BusinessCustomer();

            HttpResponseMessage? response = await _businessCustomerService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<BusinessCustomer>(jsonString);
            }

            return customer;
        }
    }
}
