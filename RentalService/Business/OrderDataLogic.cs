﻿using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using RentalService.ModelConversion;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class OrderDataLogic : IOrderData
    {
        private readonly IOrderAccess _orderAccess;

        public OrderDataLogic(IOrderAccess orderAccess)
        {
            _orderAccess = orderAccess;
        }

        public OrderDto GetById(int orderId)
        {
            try
            {
                Order order = _orderAccess.GetOrderById(orderId);
                return OrderDtoConvert.FromOrder(order);
              
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                throw;
            }
        }

        public List<OrderDto> GetAllOrders()
        {
            try
            {
                List<Order> orders = _orderAccess.GetAllOrders();
                return OrderDtoConvert.FromOrderCollection(orders);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                throw;
            }
        }

        public int CreateOrder(OrderDto newOrder)
        {
            try
            {
                Order order = OrderDtoConvert.ToOrder(newOrder);
                int newOrderID = _orderAccess.AddOrder(order);
                return newOrderID;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error adding order: {ex.Message}");
                throw;
            }
        }

        public int AddOrder(OrderDto newOrder)
        {
            try
            {
                Order order = OrderDtoConvert.ToOrder(newOrder);
                int newOrderID = _orderAccess.CreateOrder(order);
                return newOrderID;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding order: {ex.Message}");
                throw;
            }
        }

        public void RemoveOrder(int orderId)
        {
            try
            {
                _orderAccess.RemoveOrder(orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing order: {ex.Message}");
                throw;
            }
        }

    }
}
