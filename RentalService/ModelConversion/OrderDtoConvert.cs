using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public static class OrderDtoConvert
    {
        public static OrderDto FromOrder(Order order)
        {
            return new OrderDto
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                StartTime = order.StartTime,
                EndTime = order.EndTime,
                TotalHours = order.TotalHours,
                SubTotalPrice = order.SubTotalPrice,
                TotalOrderPrice = order.TotalOrderPrice
            };
        }

        public static List<OrderDto> FromOrderCollection(List<Order> orders)
        {
            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                orderDtos.Add(FromOrder(order));
            }
            return orderDtos;
        }

        public static Order ToOrder(OrderDto orderDto)
        {
            return new Order
            {
                OrderID = orderDto.OrderID,
                CustomerID = orderDto.CustomerID,
                OrderDate = orderDto.OrderDate,
                StartDate = orderDto.StartDate,
                EndDate = orderDto.EndDate,
                StartTime = orderDto.StartTime,
                EndTime = orderDto.EndTime,
                TotalHours = orderDto.TotalHours,
                SubTotalPrice = orderDto.SubTotalPrice,
                TotalOrderPrice = orderDto.TotalOrderPrice
            };
        }
    }
}
