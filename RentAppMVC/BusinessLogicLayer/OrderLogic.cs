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

        public async Task<int> AddOrder(Order order)
        {
            // Tilføj ordren og få den tildelte OrderId
            int orderID = await _orderAccess.AddOrder(order);

            // Returner den tildelte OrderId
            return orderID;
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _orderAccess.GetById(orderId);
        }
    }
}
