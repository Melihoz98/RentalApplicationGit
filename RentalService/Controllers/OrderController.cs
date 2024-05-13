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
                // Log the error
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
                // Log the error
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderDto newOrder)
        {
            try
            {
                _orderData.AddOrder(newOrder);
                return Ok("Order added successfully");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error adding order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
