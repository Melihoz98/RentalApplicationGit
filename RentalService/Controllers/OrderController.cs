﻿using Microsoft.AspNetCore.Mvc;
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
                List<OrderDto> orderDtos = _orderData.GetAllOrders();
                if (orderDtos != null && orderDtos.Count > 0)
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
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                int insertedId = _orderData.CreateOrder(orderDto);
                return CreatedAtAction(nameof(GetById), new { id = insertedId }, orderDto);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error creating order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            try
            {
                orderDto.OrderID = id;
                _orderData.UpdateOrder(orderDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderData.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error deleting order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}