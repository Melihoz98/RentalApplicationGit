using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
                // Serialize orderLine object to JSON
                string orderLineJson = JsonConvert.SerializeObject(orderLine);

                // Create StringContent from JSON
                var content = new StringContent(orderLineJson, System.Text.Encoding.UTF8, "application/json");

                // Call the API to add order line
                HttpResponseMessage response = await _orderLineService.CallServicePost(content);
                response.EnsureSuccessStatusCode(); // Throw exception if not successful
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error adding order line: {ex.Message}");
                throw; // Rethrow the exception
            }
        }

        public async Task<List<OrderLine>?> GetAllOrderLines()
        {
            try
            {
                List<OrderLine>? orderLines = null;

                // Call the API to get all order lines
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
                // Log the error
                Console.WriteLine($"Error getting all order lines: {ex.Message}");
                throw; // Rethrow the exception
            }
        }

        public async Task<OrderLine?> GetById(int orderID, string serialNumber)
        {
            try
            {
                // Pass the orderID and serialNumber to the OrderLine constructor
                OrderLine orderLine = new OrderLine(orderID, serialNumber);

                // Call the API to get order line by ID
                HttpResponseMessage response = await _orderLineService.GetById($"{orderID}/{serialNumber}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    orderLine = JsonConvert.DeserializeObject<OrderLine>(jsonString);
                }

                return orderLine;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error getting order line by ID: {ex.Message}");
                throw; // Rethrow the exception
            }
        }

    }
}
