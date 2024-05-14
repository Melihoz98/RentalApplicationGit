using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IOrderLineAccess
    {
        Task AddOrderLine(OrderLine orderLine);
        Task<List<OrderLine>?> GetAllOrderLines();
        Task<OrderLine?> GetById(int orderID, string serialNumber);
    }
}
