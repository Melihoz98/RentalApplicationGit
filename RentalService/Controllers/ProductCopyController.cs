using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCopyController : ControllerBase
    {
        private readonly IProductCopyData _productCopyData;

        public ProductCopyController(IProductCopyData productCopyData)
        {
            _productCopyData = productCopyData;
        }

        [HttpGet("{serialNumber}")]
        public IActionResult GetBySerialNumber(string serialNumber)
        {
            try
            {
                ProductCopyDto productCopyDto = _productCopyData.GetBySerialNumber(serialNumber);
                if (productCopyDto != null)
                {
                    return Ok(productCopyDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting product copy by serial number: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllProductCopies()
        {
            try
            {
                List<ProductCopyDto?>? productCopyDtos = _productCopyData.GetAllProductCopies();
                if (productCopyDtos != null)
                {
                    return Ok(productCopyDtos);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting all product copies: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("product/{productID}")]
        public IActionResult GetAllProductCopiesByProductID(int productID)
        {
            try
            {
                List<ProductCopyDto?>? productCopyDtos = _productCopyData.GetAllProductCopiesByProductID(productID);
                if (productCopyDtos != null)
                {
                    return Ok(productCopyDtos);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting all product copies by product ID: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public IActionResult CreateProductCopy([FromBody] ProductCopyDto productCopyDto)
        {
            try
            {
                _productCopyData.CreateProductCopy(productCopyDto);
                return CreatedAtAction(nameof(GetBySerialNumber), new { serialNumber = productCopyDto.SerialNumber }, productCopyDto);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error creating product copy: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        

        [HttpDelete("{serialNumber}")]
        public IActionResult DeleteProductCopy(string serialNumber)
        {
            try
            {
                _productCopyData.DeleteProductCopy(serialNumber);
                return NoContent();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting product copy: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("available/product/{productID}")]
        public IActionResult GetAllAvailableProductCopiesByDateTime(int productID, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                List<ProductCopyDto> availableProductCopies = _productCopyData.GetAllAvailableProductCopyByProductID(productID, startDate, endDate, startTime, endTime);
                if (availableProductCopies != null && availableProductCopies.Count > 0)
                {
                    return Ok(availableProductCopies);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting all available product copies by date and time: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
