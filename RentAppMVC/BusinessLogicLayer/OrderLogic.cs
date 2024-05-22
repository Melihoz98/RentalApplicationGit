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
        private readonly IProductCopyAccess _productCopyAccess;

        public OrderLogic(ProductCopyLogic productCopyLogic, OrderLineLogic orderLineLogic, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IOrderAccess orderAccess, IProductCopyAccess productCopyAccess)
        {
            _productCopyLogic = productCopyLogic;
            _userManager = userManager;
            _orderLineLogic = orderLineLogic;
            _httpContextAccessor = httpContextAccessor;
            _orderAccess = orderAccess;
            _productCopyAccess = productCopyAccess;
        }

        public async Task<int> CreateOrder(Order order)
        {
            try
            {
                var shoppingCart = CookieUtility.ReadCart(_httpContextAccessor.HttpContext);
                var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                bool cartUpdated = false;

                // Check availability and update shopping cart if necessary
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

                // Create the order object
                order.CustomerID = currentUser.Id;
                order.OrderDate = DateTime.Now;
                order.StartDate = shoppingCart.StartDate;
                order.EndDate = shoppingCart.EndDate;
                order.StartTime = shoppingCart.StartTime;
                order.EndTime = shoppingCart.EndTime;
                order.TotalHours = shoppingCart.TotalHours;
                order.SubTotalPrice = shoppingCart.SubTotalPrice;
                order.TotalOrderPrice = shoppingCart.TotalOrderPrice;
                order.OrderLines = shoppingCart.Items.ToList();

                // Add order to database via data access layer
                int orderId = await _orderAccess.AddOrder(order);

                // Empty the shopping cart
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


        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _orderAccess.GetById(orderId);
        }
    }
}
