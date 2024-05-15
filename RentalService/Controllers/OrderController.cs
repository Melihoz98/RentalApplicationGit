using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderData _orderData;

        public OrderController(IOrderData orderData)
        {
            _orderData = orderData;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                OrderDto orderDto = _orderData.GetById(id);
                if (orderDto != null)
                {
                    return Ok(orderDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            
            try
            {
                List<OrderDto?>? orderDtos = _orderData.GetAllOrders();
                if (orderDtos != null)
                {
                    return Ok(orderDtos);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderDto newOrder)
        {
            try
            {
                int orderID = _orderData.AddOrder(newOrder);
                if (orderID > 0)
                {
                    return Ok(orderID);
                }
                else
                {
                    return StatusCode(500, "Failed to add order");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error adding order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
