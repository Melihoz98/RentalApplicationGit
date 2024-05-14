using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface IOrderLineData
    {
        void AddOrderLine(OrderLineDto orderLineDto);
        void RemoveOrderLine(int orderID, string serialNumber);
       
        OrderLineDto GetOrderLineByOrderID(int orderID);
        List<OrderLineDto> GetOrderLinesBySerialNumber(string serialNumber);
    }
}
