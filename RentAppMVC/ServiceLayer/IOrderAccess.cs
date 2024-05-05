using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IOrderAccess
    {
        Task AddOrder(Order order);
        Task<Order?> GetById(int orderId);
    }
}
