using Microsoft.AspNetCore.Mvc;
using RentAppMVC.Models;
using RentAppMVC.Utilities;
using RentAppMVC.BusinessLogicLayer;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RentAppMVC.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        private readonly ProductCopyLogic _productCopyLogic;
        private readonly OrderLogic _orderLogic;
        private readonly OrderLineLogic _orderLineLogic;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(ProductCopyLogic productCopyLogic, OrderLogic orderLogic, OrderLineLogic orderLineLogic, UserManager<IdentityUser> userManager)
        {
            _productCopyLogic = productCopyLogic;
            _orderLogic = orderLogic;
            _userManager = userManager;
            _orderLineLogic = orderLineLogic;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.Message = TempData["SuccessMessage"].ToString();
            }
            else if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.Message = TempData["ErrorMessage"].ToString();
            }
            else
            {
                ViewBag.Message = "Your order has been processed successfully.";
            }

            return View();
        }


        [HttpPost("ProcessOrder")]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            try
            {
                var shoppingCart = CookieUtility.ReadCart(HttpContext);
                var currentUser = await _userManager.GetUserAsync(User);
                bool cartUpdated = false; 

                foreach (var orderLine in shoppingCart.Items.ToList()) 
                {
                    bool isProductAvailable = await CheckProductCopyAvailability(orderLine.SerialNumber, shoppingCart.StartDate, shoppingCart.EndDate, shoppingCart.StartTime, shoppingCart.EndTime);
                    if (!isProductAvailable)
                    {
                        var availableCopy = await _productCopyLogic.GetAllAvailableProductCopyByProductID(orderLine.Product.ProductID, shoppingCart.StartDate, shoppingCart.EndDate, shoppingCart.StartTime, shoppingCart.EndTime);
                        if (availableCopy != null)
                        {
                            TempData["ErrorMessage"] += $"Product {orderLine.Product.ProductName} is no longer available. ";
                            shoppingCart.Items.Remove(orderLine);
                            OrderLine newOrderLine = new OrderLine(-1, availableCopy[0].SerialNumber, orderLine.Product);
                            shoppingCart.Items.Add(newOrderLine);
                            cartUpdated = true;
                        }
                        else
                        {
                            TempData["ErrorMessage"] += $"Product {orderLine.Product.ProductName} is no longer available and has been removed from your cart. ";
                            shoppingCart.Items.Remove(orderLine);
                            cartUpdated = true;
                        }
                    }
                }

                if (cartUpdated)
                {
                    CookieUtility.UpdateCart(HttpContext, shoppingCart);
                }

                Order newOrder = new Order
                {
                    CustomerID = currentUser.Id,
                    OrderDate = DateTime.Now,
                    StartDate = shoppingCart.StartDate,
                    EndDate = shoppingCart.EndDate,
                    StartTime = shoppingCart.StartTime,
                    EndTime = shoppingCart.EndTime,
                    TotalHours = shoppingCart.TotalHours,
                    SubTotalPrice = shoppingCart.SubTotalPrice,
                    TotalOrderPrice = shoppingCart.TotalOrderPrice
                };

                int orderId = await _orderLogic.AddOrder(newOrder);

                if (orderId > 0)
                {
                    foreach (var orderLine in shoppingCart.Items)
                    {
                        orderLine.OrderID = orderId;
                        await _orderLineLogic.AddOrderLine(orderLine);
                    }
                }

                CookieUtility.EmptyCart(HttpContext);

                TempData["SuccessMessage"] = "Your order has been processed successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
                return RedirectToAction("Index");
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


        [HttpGet("Confirmation")]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
