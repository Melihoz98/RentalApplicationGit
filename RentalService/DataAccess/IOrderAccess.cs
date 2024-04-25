using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface IOrderAccess
    {
        void AddOrder(Order order);
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }
}
