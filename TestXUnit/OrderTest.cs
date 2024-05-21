using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using Xunit;

namespace RentalService.Tests
{
    public class OrderDataLogicTests : IDisposable
    {
        private readonly IOrderAccess _orderAccess;
        private readonly OrderDataLogic _orderDataLogic;
        private readonly List<int> _createdOrderIds = new List<int>();

        public OrderDataLogicTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _orderAccess = new OrderAccess(configuration);
            _orderDataLogic = new OrderDataLogic(_orderAccess);
        }

        [Fact]
        public void Test_AddOrder()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustomerID = "b6a153f9-1632-409b-8cbf-9fea955d58e4",
                OrderDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1),
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(12),
                TotalHours = 2,
                SubTotalPrice = 100,
                TotalOrderPrice = 120
            };

            // Act
            int newOrderId = _orderDataLogic.AddOrder(orderDto);

            // Assert
            Assert.True(newOrderId > 0);

            
            _createdOrderIds.Add(newOrderId);
        }

        [Fact]
        public void Test_GetById()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustomerID = "b6a153f9-1632-409b-8cbf-9fea955d58e4",
                OrderDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1),
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(12),
                TotalHours = 2,
                SubTotalPrice = 100,
                TotalOrderPrice = 120
            };

            int newOrderId = _orderDataLogic.AddOrder(orderDto);
            _createdOrderIds.Add(newOrderId);

            // Act
            var retrievedOrder = _orderDataLogic.GetById(newOrderId);

            // Assert
            Assert.NotNull(retrievedOrder);
            Assert.Equal("b6a153f9-1632-409b-8cbf-9fea955d58e4", retrievedOrder.CustomerID);
        }

        [Fact]
        public void Test_GetAllOrders()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustomerID = "b6a153f9-1632-409b-8cbf-9fea955d58e4",
                OrderDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1),
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(12),
                TotalHours = 2,
                SubTotalPrice = 100,
                TotalOrderPrice = 120
            };

            int newOrderId = _orderDataLogic.AddOrder(orderDto);
            _createdOrderIds.Add(newOrderId);

            // Act
            var orders = _orderDataLogic.GetAllOrders();

            // Assert
            Assert.NotNull(orders);
            Assert.True(orders.Count > 0, "Expected at least one order in the database.");

            
            bool orderExists = false;
            foreach (var order in orders)
            {
                if (order.OrderID == newOrderId)
                {
                    orderExists = true;
                    break;
                }
            }
            Assert.True(orderExists, $"Expected order with ID {newOrderId} to exist in the database.");

            
        }

        public void Dispose()
        {
            
            _createdOrderIds.Clear();
        }
    }
}
