using RentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    internal interface IOrderAccess
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
    }
}
