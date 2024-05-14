using Newtonsoft.Json;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public class OrderLineAccess : IOrderLineAccess
    {
        private readonly IServiceConnection _orderLineService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/OrderLine/";

        public OrderLineAccess()
        {
            _orderLineService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task AddOrderLine(OrderLine orderLine)
        {
            try
            {
                string orderLineJson = JsonConvert.SerializeObject(orderLine);

                var content = new StringContent(orderLineJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _orderLineService.CallServicePost(content);
                response.EnsureSuccessStatusCode(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order line: {ex.Message}");
                throw; 
            }
        }

        public async Task<List<OrderLine>?> GetAllOrderLines()
        {
            try
            {
                List<OrderLine>? orderLines = null;

                HttpResponseMessage response = await _orderLineService.CallServiceGet();
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    orderLines = JsonConvert.DeserializeObject<List<OrderLine>>(jsonString);
                }

                return orderLines;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all order lines: {ex.Message}");
                throw;
            }
        }

        public async Task<OrderLine?> GetById(int orderID, string serialNumber, Product product)
        {
            try
            {
                OrderLine orderLine = new OrderLine(orderID, serialNumber, product);

                HttpResponseMessage response = await _orderLineService.GetById($"{orderID}/{serialNumber}/{product}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    orderLine = JsonConvert.DeserializeObject<OrderLine>(jsonString);
                }

                return orderLine;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting order line by ID: {ex.Message}");
                throw; 
            }
        }

    }
}
