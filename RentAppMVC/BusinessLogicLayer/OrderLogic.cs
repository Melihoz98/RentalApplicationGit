using Microsoft.AspNetCore.Identity;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;
using RentAppMVC.Utilities;
using System.Transactions;


namespace RentAppMVC.BusinessLogicLayer
{
    public class OrderLogic
    {
        private readonly IOrderAccess _orderAccess;
        private readonly ProductCopyLogic _productCopyLogic;
        private readonly OrderLineLogic _orderLineLogic;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderLogic(ProductCopyLogic productCopyLogic, OrderLineLogic orderLineLogic, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IOrderAccess orderAccess)
        {
            _productCopyLogic = productCopyLogic;
            _userManager = userManager;
            _orderLineLogic = orderLineLogic;
            _httpContextAccessor = httpContextAccessor;
            _orderAccess = orderAccess;
        }

        public async Task<int> CreateOrder(Order order)
        {
            try
            {
                var shoppingCart = CookieUtility.ReadCart(_httpContextAccessor.HttpContext);
                var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                bool cartUpdated = false;

                // Flyt CheckProductCopyAvailability-metoden uden for TransactionScope
                foreach (var orderLine in shoppingCart.Items.ToList())
                {
                    bool isProductAvailable = await CheckProductCopyAvailability(orderLine.SerialNumber, shoppingCart.StartDate, shoppingCart.EndDate, shoppingCart.StartTime, shoppingCart.EndTime);
                    if (!isProductAvailable)
                    {
                        var availableCopy = await _productCopyLogic.GetAllAvailableProductCopyByProductID(orderLine.Product.ProductID, shoppingCart.StartDate, shoppingCart.EndDate, shoppingCart.StartTime, shoppingCart.EndTime);
                        if (availableCopy != null)
                        {
                            shoppingCart.Items.Remove(orderLine);
                            OrderLine newOrderLine = new OrderLine(-1, availableCopy[0].SerialNumber, orderLine.Product);
                            shoppingCart.Items.Add(newOrderLine);
                            cartUpdated = true;
                        }
                        else
                        {
                            shoppingCart.Items.Remove(orderLine);
                            cartUpdated = true;
                        }
                    }
                }

                if (cartUpdated)
                {
                    CookieUtility.UpdateCart(_httpContextAccessor.HttpContext, shoppingCart);
                }

                order.CustomerID = currentUser.Id;
                order.OrderDate = DateTime.Now;
                order.StartDate = shoppingCart.StartDate;
                order.EndDate = shoppingCart.EndDate;
                order.StartTime = shoppingCart.StartTime;
                order.EndTime = shoppingCart.EndTime;
                order.TotalHours = shoppingCart.TotalHours;
                order.SubTotalPrice = shoppingCart.SubTotalPrice;
                order.TotalOrderPrice = shoppingCart.TotalOrderPrice;

                int orderId = await AddOrder(order);

                if (orderId > 0)
                {
                    foreach (var orderLine in shoppingCart.Items)
                    {
                        orderLine.OrderID = orderId;
                        await _orderLineLogic.AddOrderLine(orderLine);
                    }
                }

                CookieUtility.EmptyCart(_httpContextAccessor.HttpContext);

                return orderId;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the order.", ex);
            }
        }

        public async Task<bool> CheckProductCopyAvailability(string serialNumber, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            var productCopy = await _productCopyLogic.GetProductCopyBySerialNumber(serialNumber);

            if (productCopy == null)
            {
                return false;
            }

            var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productCopy.ProductID, startDate, endDate, startTime, endTime);

            return availableProductCopies.Any(pc => pc.SerialNumber == serialNumber);
        }

        private async Task<int> AddOrder(Order order)
        {
            return await _orderAccess.AddOrder(order);
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _orderAccess.GetById(orderId);
        }
    }
}
