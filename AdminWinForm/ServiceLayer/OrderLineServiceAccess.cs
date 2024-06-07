using AdminWinForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    public class OrderLineServiceAccess : IOrderLineAccess
    {
        private readonly IServiceConnection _orderLineService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/OrderLine/order/";

        public OrderLineServiceAccess()
        {
            _orderLineService = new ServiceConnection(_serviceBaseUrl);
        }


        public async Task<List<OrderLine>> GetOrderLinesByOrderId(int orderId)
        {
            List<OrderLine> orderLines = new List<OrderLine>();

            _orderLineService.UseUrl = $"{_serviceBaseUrl}{orderId}";
            HttpResponseMessage? response = await _orderLineService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                orderLines = JsonConvert.DeserializeObject<List<OrderLine>>(jsonString);
            }
          
            return orderLines;
        }


    }
}
