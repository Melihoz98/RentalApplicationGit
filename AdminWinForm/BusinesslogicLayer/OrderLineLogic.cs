using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;

namespace AdminWinForm.BusinesslogicLayer
{
    public class OrderLineLogic
    {
        readonly IOrderLineAccess _orderLineAccess;
        
        
        public OrderLineLogic() 
        {
            _orderLineAccess = new OrderLineServiceAccess();
        }

        public async Task<List<OrderLine>> GetOrderLinesByOrderId(int orderId)
        {
            List<OrderLine>? foundOrderLines = null;
            if(_orderLineAccess != null)
            {
                foundOrderLines = await _orderLineAccess.GetOrderLinesByOrderId(orderId);
            }

            return foundOrderLines;
        }
        
    }


}
