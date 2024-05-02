using RentalService.Models;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public interface IOrderAccess
    {
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
    }
}
