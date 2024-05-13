using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                ProductDto productDto = _productData.GetById(id);
                if (productDto != null)
                {
                    return Ok(productDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error getting product by ID: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<ProductDto?>? productDtos = _productData.GetAllProducts();
                if (productDtos != null)
                {
                    return Ok(productDtos);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error getting all products: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                int insertedId = _productData.CreateProduct(productDto);
                return CreatedAtAction(nameof(GetById), new { id = insertedId }, productDto);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error creating product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productData.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error deleting product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
