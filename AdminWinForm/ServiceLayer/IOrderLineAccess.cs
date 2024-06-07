using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    internal interface IOrderLineAccess
    {
        Task<List<OrderLine>> GetOrderLinesByOrderId(int orderId);
    }
}
