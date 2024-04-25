using RentalService.DTO;
using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class OrderLineDtoConvert
    {
        public static OrderLineDto FromOrderLine(OrderLine orderLine)
        {
            return new OrderLineDto
            {
                OrderID = orderLine.OrderID,
                SerialNumber = orderLine.SerialNumber
            };
        }

        public static OrderLine ToOrderLine(OrderLineDto orderLineDto)
        {
            return new OrderLine(
                orderLineDto.OrderID,
                orderLineDto.SerialNumber
            );
        }

        public static List<OrderLineDto> FromOrderLineCollection(List<OrderLine> orderLines)
        {
            var orderLineDtos = new List<OrderLineDto>();
            foreach (var orderLine in orderLines)
            {
                orderLineDtos.Add(FromOrderLine(orderLine));
            }
            return orderLineDtos;
        }
    }
}
