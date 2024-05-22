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
                OrderDate = (DateTime)order.OrderDate,
                StartDate = (DateTime)order.StartDate,
                EndDate = (DateTime)order.EndDate,
                StartTime = (TimeSpan)order.StartTime,
                EndTime = (TimeSpan)order.EndTime,
                TotalHours = (int)order.TotalHours,
                SubTotalPrice = (decimal)order.SubTotalPrice,
                TotalOrderPrice = (decimal)order.TotalOrderPrice,
                OrderLines = order.OrderLines?.Select(ol => new OrderLineDto
                {
                    OrderID = ol.OrderID,
                    SerialNumber = ol.SerialNumber,
                    Product = ProductDtoConvert.FromProduct(ol.Product)  // Assuming you have a ProductDtoConvert class
                }).ToList()
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
                TotalOrderPrice = orderDto.TotalOrderPrice,
                OrderLines = orderDto.OrderLines?.Select(ol => new OrderLine
                {
                    OrderID = ol.OrderID,
                    SerialNumber = ol.SerialNumber,
                    Product = ProductDtoConvert.ToProduct(ol.Product)  // Assuming you have a ProductDtoConvert class
                }).ToList()
            };
        }
    }
}
