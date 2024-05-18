using Newtonsoft.Json;
using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    public class OrderServiceAccess : IOrderAccess
    {
        private readonly IServiceConnection _orderService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/Order/";

        public OrderServiceAccess()
        {
            _orderService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            HttpResponseMessage? response = await _orderService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<List<Order>>(jsonString);
            }

            return orders;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            Order order = new Order();

            _orderService.UseUrl = $"{_serviceBaseUrl}/{orderId}";
            HttpResponseMessage? response = await _orderService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                order = JsonConvert.DeserializeObject<Order>(jsonString);
            }

            return order;
        }

    }
}
