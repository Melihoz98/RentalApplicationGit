using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface IOrderData
    {
        OrderDto GetById(int orderId);
        List<OrderDto> GetAllOrders();
    }
}
