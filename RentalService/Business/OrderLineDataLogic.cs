using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class OrderLineDataLogic : IOrderLineData
    {
        private readonly IOrderLineAccess _orderLineAccess;

        public OrderLineDataLogic(IOrderLineAccess orderLineAccess)
        {
            _orderLineAccess = orderLineAccess;
        }

        public void AddOrderLine(OrderLineDto orderLineDto)
        {
            try
            {
                OrderLine orderLine = new OrderLine(orderLineDto.OrderID, orderLineDto.SerialNumber);
                _orderLineAccess.AddOrderLine(orderLine);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error adding order line: {ex.Message}");
                throw;
            }
        }

        public void RemoveOrderLine(int orderID, string serialNumber)
        {
            try
            {
                _orderLineAccess.RemoveOrderLine(orderID, serialNumber);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error removing order line: {ex.Message}");
                throw;
            }
        }

        public void UpdateOrderLine(OrderLineDto orderLineDto)
        {
            try
            {
                OrderLine orderLine = new OrderLine(orderLineDto.OrderID, orderLineDto.SerialNumber);
                _orderLineAccess.UpdateOrderLine(orderLine);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating order line: {ex.Message}");
                throw;
            }
        }

        public OrderLineDto GetOrderLineByOrderID(int orderID)
        {
            try
            {
                OrderLine orderLine = _orderLineAccess.GetOrderLineByOrderID(orderID);
                return orderLine != null ? new OrderLineDto { OrderID = orderLine.OrderID, SerialNumber = orderLine.SerialNumber } : null;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error getting order line by order ID: {ex.Message}");
                throw;
            }
        }

        public List<OrderLineDto> GetOrderLinesBySerialNumber(string serialNumber)
        {
            try
            {
                List<OrderLine> orderLines = _orderLineAccess.GetOrderLinesBySerialNumber(serialNumber);
                List<OrderLineDto> orderLineDtos = new List<OrderLineDto>();
                foreach (var orderLine in orderLines)
                {
                    orderLineDtos.Add(new OrderLineDto { OrderID = orderLine.OrderID, SerialNumber = orderLine.SerialNumber });
                }
                return orderLineDtos;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error getting order lines by serial number: {ex.Message}");
                throw;
            }
        }
    }
}
