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
        private readonly OrderLogic _orderLogic;
        private readonly OrderLineLogic _orderLineLogic;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(OrderLogic orderLogic, OrderLineLogic orderLineLogic, UserManager<IdentityUser> userManager)
        {
            _orderLogic = orderLogic;
            _userManager = userManager;
            _orderLineLogic = orderLineLogic;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            // Check if TempData contains a success or error message
            if (TempData.ContainsKey("SuccessMessage"))
            {
                // Display success message to the user
                ViewBag.Message = TempData["SuccessMessage"].ToString();
            }
            else if (TempData.ContainsKey("ErrorMessage"))
            {
                // Display error message to the user
                ViewBag.Message = TempData["ErrorMessage"].ToString();
            }
            else
            {
                // No message, display default message or empty string
                ViewBag.Message = "Your order has been processed successfully."; // Default message
            }

            return View();
        }


        //[HttpPost("ProcessOrder")]
        //public async Task<ActionResult> CreateOrder(Order order)
        //{
        //    try
        //    {
        //        // Get the shopping cart from the cookie
        //        var shoppingCart = CookieUtility.ReadCart(HttpContext);

        //        // Get the current user
        //        var currentUser = await _userManager.GetUserAsync(User);

        //        // Create a new order based on the shopping cart information
        //        Order newOrder = new Order
        //        {
        //            CustomerID = currentUser.Id, // Set the customer's ID (you may change this to fit your implementation)
        //            OrderDate = DateTime.Now, // Set the order date to the current time
        //            StartDate = shoppingCart.StartDate,
        //            EndDate = shoppingCart.EndDate,
        //            StartTime = shoppingCart.StartTime,
        //            EndTime = shoppingCart.EndTime,
        //            TotalHours = shoppingCart.TotalHours,
        //            SubTotalPrice = shoppingCart.SubTotalPrice,
        //            TotalOrderPrice = shoppingCart.TotalOrderPrice
        //        };

        //        // Save the new order to the database
        //        await _orderLogic.AddOrder(newOrder);

        //        // Ensure that OrderId is assigned
        //        if (newOrder.OrderID > 0)
        //        {
        //            // Add each order line to the new order
        //            foreach (var orderLine in shoppingCart.Items)
        //            {
        //                // Add OrderId to OrderLine before creation
        //                orderLine.OrderID = newOrder.OrderID;

        //                // Create a new OrderLine object using the constructor
        //                OrderLine newOrderLine = new OrderLine(orderLine.OrderID, orderLine.SerialNumber);

        //                // Save the new order line to the database
        //                // Note: You may need to adjust this based on how you handle saving order lines
        //                await _orderLineLogic.AddOrderLine(newOrderLine);
        //            }
        //        }

        //        // Clear the shopping cart after the order is saved
        //        CookieUtility.EmptyCart(HttpContext);

        //        TempData["SuccessMessage"] = "Your order has been processed successfully.";
        //        // Redirect to a confirmation page or another relevant page
        //        return RedirectToAction("Index");

        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle errors and show an error message
        //        TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
        //        return RedirectToAction("Index");

        //    }
        //}

        [HttpPost("ProcessOrder")]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            try
            {
                // Get the shopping cart from the cookie
                var shoppingCart = CookieUtility.ReadCart(HttpContext);

                // Get the current user
                var currentUser = await _userManager.GetUserAsync(User);

                // Create a new order based on the shopping cart information
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

                // Save the new order to the database and get the assigned OrderId
                int orderId = await _orderLogic.AddOrder(newOrder);

                // Ensure that OrderId is assigned
                if (orderId > 0)
                {
                    // Add each order line to the new order
                    foreach (var orderLine in shoppingCart.Items)
                    {
                        // Add the retrieved OrderId to OrderLine before creation
                        orderLine.OrderID = orderId;

                        // Save the new order line to the database
                        await _orderLineLogic.AddOrderLine(orderLine);
                    }
                }

                // Clear the shopping cart after the order is saved
                CookieUtility.EmptyCart(HttpContext);

                TempData["SuccessMessage"] = "Your order has been processed successfully.";
                // Redirect to a confirmation page or another relevant page
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle errors and show an error message
                TempData["ErrorMessage"] = "An error occurred while processing your order. Please try again later.";
                return RedirectToAction("Index");
            }
        }



        [HttpGet("Confirmation")]
        public IActionResult Confirmation()
        {
            // Show a confirmation page
            return View();
        }
    }
}
