using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface IOrderAccess
    {
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        int AddOrder(Order newOrder);

        void RemoveOrder(int orderId); // New method for removing an order
    }
}
