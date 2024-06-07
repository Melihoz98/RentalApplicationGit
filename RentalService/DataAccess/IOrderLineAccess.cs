using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface IOrderLineAccess
    {
        void AddOrderLine(OrderLine orderLine);
        void RemoveOrderLine(int orderID, string serialNumber);
        
        OrderLine GetOrderLineByOrderID(int orderID);
        List<OrderLine> GetOrderLinesBySerialNumber(string serialNumber);
        List<OrderLine> GetOrderLinesByOrderID(int orderID);
    }
}
