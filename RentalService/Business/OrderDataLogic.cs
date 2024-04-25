using RentalService.DataAccess;
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
                if (order != null)
                {
                    return OrderDtoConvert.FromOrder(order);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log the error
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
                // Log the error
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                throw;
            }
        }

        public int CreateOrder(OrderDto orderToAdd)
        {
            try
            {
                Order order = OrderDtoConvert.ToOrder(orderToAdd);
                _orderAccess.AddOrder(order);
                return order.OrderID;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error creating order: {ex.Message}");
                throw;
            }
        }

        public void UpdateOrder(OrderDto orderToUpdate)
        {
            try
            {
                Order order = OrderDtoConvert.ToOrder(orderToUpdate);
                _orderAccess.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating order: {ex.Message}");
                throw;
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                _orderAccess.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error deleting order: {ex.Message}");
                throw;
            }
        }
    }
}
