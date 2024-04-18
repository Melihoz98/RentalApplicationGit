using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
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

        [HttpGet]
        public IActionResult Get()
        {
            List<ProductDto?>? foundProducts = _productData.GetAllProducts();

            if (foundProducts != null && foundProducts.Count > 0)
            {
                return Ok(foundProducts);
            }
            else
            {
                return NoContent(); // No products found
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductDto? product = _productData.GetByID(id);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound(); // Product not found
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto productDto)
        {
            try
            {
                _productData.AddProduct(productDto);
                // Return 201 Created status without including ProductDto in response
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


[HttpPut("{id}")]
public IActionResult Put(int id, [FromBody] ProductDto productDto)
{
    try
    {
        // Kontrollér, om det modtagne id matcher id'et i productDto
        if (id != productDto.ProductID)
        {
            return BadRequest("Product ID mismatch");
        }

        _productData.UpdateProduct(productDto);
        return NoContent();
    }
    catch (Exception ex)
    {
        // Håndter undtagelsen
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
}



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productData.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
