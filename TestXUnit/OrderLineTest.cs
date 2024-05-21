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
    public class OrderLineDataLogicTests : IDisposable
    {
        private readonly IOrderLineAccess _orderLineAccess;
        private readonly OrderLineDataLogic _orderLineDataLogic;
        private readonly List<(int orderID, string serialNumber)> _createdOrderLines = new List<(int orderID, string serialNumber)>();

        public OrderLineDataLogicTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _orderLineAccess = new OrderLineAccess(configuration);
            _orderLineDataLogic = new OrderLineDataLogic(_orderLineAccess);
        }

        [Fact]
        public void Test_AddOrderLine()
        {
            // Arrange
            var orderLineDto = new OrderLineDto
            {
                OrderID = 15,
                SerialNumber = "CSK17"
            };

            // Act
            _orderLineDataLogic.AddOrderLine(orderLineDto);

            // Assert
            var createdOrderLine = _orderLineDataLogic.GetOrderLineByOrderID(15);
            Assert.NotNull(createdOrderLine);
            Assert.Equal(15, createdOrderLine.OrderID);
            Assert.Equal("CSK17", createdOrderLine.SerialNumber);

            
            _createdOrderLines.Add((15, "CSK17"));
        }

        [Fact]
        public void Test_RemoveOrderLine()
        {
            // Arrange
            var orderLineDto = new OrderLineDto
            {
                OrderID = 2,
                SerialNumber = "CSK17"
            };
            _orderLineDataLogic.AddOrderLine(orderLineDto);
            _createdOrderLines.Add((2, "CSK17"));

            // Act
            _orderLineDataLogic.RemoveOrderLine(2, "CSK17");
            var deletedOrderLine = _orderLineDataLogic.GetOrderLineByOrderID(2);

            // Assert
            Assert.Null(deletedOrderLine);
        }

        [Fact]
        public void Test_GetOrderLineByOrderID()
        {
            // Arrange
            var orderLineDto = new OrderLineDto
            {
                OrderID = 3,
                SerialNumber = "CSK17"
            };
            _orderLineDataLogic.AddOrderLine(orderLineDto);
            _createdOrderLines.Add((3, "CSK17"));

            // Act
            var retrievedOrderLine = _orderLineDataLogic.GetOrderLineByOrderID(3);

            // Assert
            Assert.NotNull(retrievedOrderLine);
            Assert.Equal(3, retrievedOrderLine.OrderID);
            Assert.Equal("CSK17", retrievedOrderLine.SerialNumber);
        }

        [Fact]
        public void Test_GetOrderLinesBySerialNumber()
        {
            // Arrange
            var orderLineDto1 = new OrderLineDto
            {
                OrderID = 4,
                SerialNumber = "CSK17"
            };
            var orderLineDto2 = new OrderLineDto
            {
                OrderID = 5,
                SerialNumber = "CSK17d"
            };
            _orderLineDataLogic.AddOrderLine(orderLineDto1);
            _orderLineDataLogic.AddOrderLine(orderLineDto2);
            _createdOrderLines.Add((4, "CSK17"));
            _createdOrderLines.Add((5, "CSK17d"));

            // Act
            var orderLines = _orderLineDataLogic.GetOrderLinesBySerialNumber("CSK17");

            // Assert
            Assert.NotNull(orderLines);
            Assert.Equal(2, orderLines.Count);
            Assert.All(orderLines, ol => Assert.Equal("CSK17", ol.SerialNumber));
        }

        public void Dispose()
        {
            
            foreach (var (orderID, serialNumber) in _createdOrderLines)
            {
                _orderLineDataLogic.RemoveOrderLine(orderID, serialNumber);
            }
        }
    }
}
