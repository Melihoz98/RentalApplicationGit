using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineData _orderLineData;

        public OrderLineController(IOrderLineData orderLineData)
        {
            _orderLineData = orderLineData;
        }

        [HttpPost]
        public IActionResult AddOrderLine([FromBody] OrderLineDto orderLineDto)
        {
            try
            {
                _orderLineData.AddOrderLine(orderLineDto);
                return Ok();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error adding order line: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{orderID}/{serialNumber}")]
        public IActionResult RemoveOrderLine(int orderID, string serialNumber)
        {
            try
            {
                _orderLineData.RemoveOrderLine(orderID, serialNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error removing order line: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        

        [HttpGet("{orderID}")]
        public IActionResult GetOrderLineByOrderID(int orderID)
        {
            try
            {
                OrderLineDto orderLineDto = _orderLineData.GetOrderLineByOrderID(orderID);
                if (orderLineDto != null)
                {
                    return Ok(orderLineDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting order line by order ID: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("serial/{serialNumber}")]
        public IActionResult GetOrderLinesBySerialNumber(string serialNumber)
        {
            try
            {
                List<OrderLineDto> orderLineDtos = _orderLineData.GetOrderLinesBySerialNumber(serialNumber);
                if (orderLineDtos != null && orderLineDtos.Count > 0)
                {
                    return Ok(orderLineDtos);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting order lines by serial number: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
