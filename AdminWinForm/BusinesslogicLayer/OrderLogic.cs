using AdminWinForm.ServiceLayer;
using RentalService.Models;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;
namespace AdminWinForm.BusinesslogicLayer
{
    public class OrderLogic
    {
        readonly IOrderAccess _orderAccess;

        public OrderLogic()
        {
            _orderAccess = new OrderServiceAccess();
        }

        public async Task<List<Order>?> GetAllOrders()
        {
            List<Order>? foundOrders = null;
            if (_orderAccess != null)
            {
                foundOrders = await _orderAccess.GetAllOrders();
            }
            return foundOrders;
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _orderAccess.GetOrderById(orderId);
        }
    }
}
