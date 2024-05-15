using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IOrderAccess
    {
        Task <int> AddOrder(Order order);
        Task<Order?> GetById(int orderId);
    }
}
