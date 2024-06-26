﻿using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface IOrderData
    {
        OrderDto? GetById(int id);
        List<OrderDto?>? GetAllOrders();
        int CreateOrder(OrderDto newOrder);
        void RemoveOrder(int id);
    }
}
