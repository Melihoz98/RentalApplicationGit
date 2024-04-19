using Microsoft.AspNetCore.Http;
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
        private readonly IProductData _businessLogicCtrl;

        public ProductController(IProductData inBusinessLogicCtrl)
        {
            _businessLogicCtrl = inBusinessLogicCtrl;
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> Get()
        {
            ActionResult<List<ProductDto>> foundReturn;
            List<ProductDto?>? foundProducts = _businessLogicCtrl.Get();
            if (foundProducts != null)
            {
                if (foundProducts.Count > 0)
                {
                    foundReturn = Ok(foundProducts);
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);            // Internal server error
            }
            return foundReturn;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<ProductDto> Get(int id)
        {
            ProductDto foundProduct = _businessLogicCtrl.Get(id);

            if (foundProduct != null)
            {
                return Ok(foundProduct); // Statuscode 200
            }
            else
            {
                return NotFound(); // Statuscode 404
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto productDto)
        {
            try
            {
                _businessLogicCtrl.Add(productDto);
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

                _businessLogicCtrl.Put(productDto);
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
                _businessLogicCtrl.DeleteProduct(id);
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
