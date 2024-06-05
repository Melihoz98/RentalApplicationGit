using Newtonsoft.Json;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RentAppMVC.BusinessLogicLayer
{
    public class ShoppingCartLogic
    {
        private readonly ProductCopyLogic _productCopyLogic;
        private readonly ProductLogic _productLogic;

        public ShoppingCartLogic(ProductCopyLogic productCopyLogic, ProductLogic productLogic)
        {
            _productCopyLogic = productCopyLogic;
            _productLogic = productLogic;
        }

        public async Task<ShoppingCart> AddToCart(HttpContext httpContext, int productId)
        {
            var shoppingCart = CookieUtility.ReadCart(httpContext);

            bool productAlreadyExists = shoppingCart.Items.Any(item => item.Product.ProductID == productId);

            if (!productAlreadyExists)
            {
                if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
                {
                    shoppingCart.Product = await _productLogic.GetProductById(productId);
                    CookieUtility.UpdateCart(httpContext, shoppingCart);
                    return null;
                }

                DateTime startDate = shoppingCart.StartDate;
                DateTime endDate = shoppingCart.EndDate;
                TimeSpan startTime = shoppingCart.StartTime;
                TimeSpan endTime = shoppingCart.EndTime;

                var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, startDate, endDate, startTime, endTime);
                if (availableProductCopies == null || availableProductCopies.Count == 0)
                {
                    Product thisProduct = await _productLogic.GetProductById(productId);
                    string productName = thisProduct.ProductName;
                    throw new InvalidOperationException($"The product '{productName}' is not available for the selected date and time.");
                }

                var firstProductCopy = availableProductCopies[0];
                Product product = await _productLogic.GetProductById(productId);
                OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber, product);
                shoppingCart.Items.Add(orderLine);

                CookieUtility.UpdateCart(httpContext, shoppingCart);
            }
            else
            {
                throw new InvalidOperationException("This product is already in your cart.");
            }

            return shoppingCart;
        }

        public async Task<ShoppingCart> ConfirmDateTime(HttpContext httpContext, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            var shoppingCart = CookieUtility.ReadCart(httpContext);

            if (shoppingCart.StartDate == default || shoppingCart.EndDate == default || shoppingCart.StartTime == default || shoppingCart.EndTime == default)
            {
                shoppingCart.StartDate = startDate;
                shoppingCart.EndDate = endDate;
                shoppingCart.StartTime = startTime;
                shoppingCart.EndTime = endTime;
                CookieUtility.UpdateCart(httpContext, shoppingCart);
            }

            int productId = shoppingCart.Product.ProductID;

            DateTime cartStartDate = shoppingCart.StartDate;
            DateTime cartEndDate = shoppingCart.EndDate;
            TimeSpan cartStartTime = shoppingCart.StartTime;
            TimeSpan cartEndTime = shoppingCart.EndTime;

            var availableProductCopies = await _productCopyLogic.GetAllAvailableProductCopyByProductID(productId, cartStartDate, cartEndDate, cartStartTime, cartEndTime);
            if (availableProductCopies == null || availableProductCopies.Count == 0)
            {
                CookieUtility.RemoveDateTimeFromCart(httpContext);
                throw new InvalidOperationException("This product is not available for the selected date and time.");
            }

            var firstProductCopy = availableProductCopies[0];
            Product product = await _productLogic.GetProductById(productId);
            OrderLine orderLine = new OrderLine(-1, firstProductCopy.SerialNumber, product);
            shoppingCart.Items.Add(orderLine);

            CookieUtility.UpdateCart(httpContext, shoppingCart);

            return shoppingCart;
        }

        public ShoppingCart ReadCartFromCookie(HttpContext httpContext)
        {
            var cartJson = httpContext.Request.Cookies["shoppingCart"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cartData = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
                return cartData;
            }
            return new ShoppingCart();
        }
        public ShoppingCart RemoveItem(HttpContext httpContext, string serialNumber)
        {
            var shoppingCart = CookieUtility.ReadCart(httpContext);
            shoppingCart.RemoveItem(serialNumber);
            CookieUtility.UpdateCart(httpContext, shoppingCart);
            return shoppingCart;
        }

        public void EmptyCart(HttpContext httpContext)
        {
            CookieUtility.EmptyCart(httpContext);
        }
    }
}
