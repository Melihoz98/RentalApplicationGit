using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public class OrderAccess : IOrderAccess
    {
        private readonly IServiceConnection _orderService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/Order/";

        public OrderAccess()
        {
            _orderService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<int> AddOrder(Order order)
        {
            try
            {
                string orderJson = JsonConvert.SerializeObject(order);
                var content = new StringContent(orderJson, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _orderService.CallServicePost(content);
                response.EnsureSuccessStatusCode();
                int orderID = int.Parse(await response.Content.ReadAsStringAsync());
                return orderID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
                throw;
            }
        }

        public async Task<Order?> GetById(int orderId)
        {
            try
            {
                Order order = new Order();
                HttpResponseMessage response = await _orderService.GetById(orderId.ToString());
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<Order>(jsonString);
                }
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                throw;
            }
        }
    }
}