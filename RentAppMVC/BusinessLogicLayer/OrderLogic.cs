using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class OrderLogic
    {
        private readonly IOrderAccess _orderAccess;

        public OrderLogic()
        {
            _orderAccess = new OrderAccess();
        }

        public async Task AddOrder(Order order)
        {
            await _orderAccess.AddOrder(order);
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _orderAccess.GetById(orderId);
        }
    }
}
