using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class OrderLineLogic
    {
        private readonly IOrderLineAccess _orderLineAccess;

        public OrderLineLogic()
        {
            _orderLineAccess = new OrderLineAccess();
        }

        public async Task AddOrderLine(OrderLine orderLine)
        {
            await _orderLineAccess.AddOrderLine(orderLine);
        }

        public async Task<List<OrderLine>?> GetAllOrderLines()
        {
            return await _orderLineAccess.GetAllOrderLines();
        }

        public async Task<OrderLine?> GetOrderLineById(int orderID, string serialNumber)
        {
            return await _orderLineAccess.GetById(orderID, serialNumber);
        }
    }
}